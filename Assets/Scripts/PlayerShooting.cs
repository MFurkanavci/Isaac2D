using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private HeroSO hero;

    [Header("Player Components")]
    private Rigidbody2D rb;
    public GameObject slash;
    public GameObject bulletPoint;

    [Header("Shooting")]
    public float fireRate = 0.5f;
    public float bulletLifetime = 2f;
    public float bulletSpeed = 10f;
    public float fireTimer;

    private Vector2 direction;

    private bool hasAttack2;

    [Header("Bullet Pooling")]
    public string bulletTag = "Bullet";

    // New variable for breath point
    public float animationBreathTime = 0.1f; // Adjust this for the idle/run time between attacks

    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitializePlayerShooting(HeroSO hero)
    {
        this.hero = hero;
        fireRate = hero.attackSpeed;
        bulletSpeed = hero.bulletSpeed;

        if (!hero.projectile)
        {
            slash = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        }
        else
        {
            bulletPoint = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        }

        foreach (var parameter in Player.Instance.anim.parameters)
        {
            if (parameter.name == "Attack2")
            {
                hasAttack2 = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (hero == null) return;

        direction = GetCameraDirection();

        if (Input.GetMouseButton(0) && fireTimer <= 0)
        {
            if (!isAttacking) // Only shoot if not in the middle of an attack
            {
                if (hero.projectile)
                {
                    StartCoroutine(ShootWithTransition(direction));
                }
                else
                {
                    StartCoroutine(SlashWithTransition(direction));
                }
            }
        }

        if (fireTimer > -0.5f)
        {
            fireTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SpecialAttack();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UltimateAttack();
        }
    }

    private Vector2 GetCameraDirection()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = (mousePos - screenPoint).normalized;
        return direction;
    }

    // Coroutine for shooting with transition back to idle or running
    private IEnumerator ShootWithTransition(Vector2 direction)
    {
        isAttacking = true; // Mark as attacking to prevent multiple triggers

        // Trigger attack animation
        Shoot(direction);

        gameObject.GetComponentInChildren<SpriteRenderer>().flipX = direction.x < 0;

        // Wait for the animation to complete based on fireRate (or custom length)
        yield return new WaitForSeconds(fireRate);

        // Transition to idle or running based on movement
        TransitionToMovementState();

        // Allow a slight pause before the next attack
        yield return new WaitForSeconds(animationBreathTime);

        fireTimer = fireRate;
        isAttacking = false; // Attack cycle complete
    }

    private IEnumerator SlashWithTransition(Vector2 direction)
    {
        isAttacking = true;

        // Trigger slash animation
        Slash(direction);

        gameObject.GetComponentInChildren<SpriteRenderer>().flipX = direction.x < 0;

        // Wait for animation to complete
        yield return new WaitForSeconds(fireRate);

        // Transition to idle or running
        TransitionToMovementState();

        // Pause before next attack
        yield return new WaitForSeconds(animationBreathTime);

        fireTimer = fireRate;
        isAttacking = false;
    }

    private void TransitionToMovementState()
    {
        // Check if player is moving, and transition to running if so
        if (rb.velocity.magnitude > 0.1f)
        {
            TriggerAnim("Run"); // Go back to running if moving
        }
        else
        {
            TriggerAnim("Idle"); // Go back to idle if standing still
        }
    }

    public void Slash(Vector2 direction)
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().flipX = direction.x < 0;

        slash.SetActive(true);

        CycleAttacks();
    }

    public void Shoot(Vector2 direction)
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().flipX = direction.x < 0;

        // Get bullet from object pool
        GameObject bullet = ObjectPool.Instance.GetFromPool(bulletTag, bulletPoint.transform.position, Quaternion.identity);

        // Set bullet position and rotation
        bullet.transform.position = bulletPoint.transform.position;
        bullet.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        // Set bullet damage, speed and lifetime
        bullet.GetComponent<Bullet>().InitializeBullet(hero.attackDamage, bulletSpeed, bulletLifetime);

        // Set bullet direction
        bullet.GetComponent<Bullet>().SetDirection(direction);

        // Set bullet active
        bullet.SetActive(true);

        // Play shooting animation
        TriggerAnim("Attack1");

        // Set animation speed
        ShootingAnimationSpeed();

        CycleAttacks();
    }

    public void ShootingAnimationSpeed()
    {
        float speed = 1 / fireRate;
        SetFloatAnim("animSpeed", speed);
    }

    bool firstAttack = false;

    public void CycleAttacks()
    {
        if (hasAttack2)
        {
            if (firstAttack)
            {
                TriggerAnim("Attack2");
                firstAttack = false;
            }
            else
            {
                TriggerAnim("Attack1");
                firstAttack = true;
            }
        }
        else
        {
            TriggerAnim("Attack1");
        }

        ShootingAnimationSpeed();
    }

    public void BoolAnim(string boolName = "", bool value = false)
    {
        Player.Instance.anim.SetBool(boolName, value);
    }

    public void TriggerAnim(string triggerName = "")
    {
        Player.Instance.anim.SetTrigger(triggerName);
    }

    public void SetFloatAnim(string floatName = "", float value = 0)
    {
        Player.Instance.anim.SetFloat(floatName, value);
    }

    public void SetIntAnim(string intName = "", int value = 0)
    {
        Player.Instance.anim.SetInteger(intName, value);
    }

    public void SpecialAttack()
    {
        StartCoroutine(PerformAttackWithBreath("SpecialAttack"));
    }

    public void UltimateAttack()
    {
        StartCoroutine(PerformAttackWithBreath("UltimateAttack"));
    }

    private IEnumerator PerformAttackWithBreath(string attackAnimation)
    {
        isAttacking = true;

        // Trigger attack animation
        TriggerAnim(attackAnimation);
        gameObject.GetComponentInChildren<SpriteRenderer>().flipX = direction.x < 0;

        // Wait for the animation to complete
        yield return new WaitForSeconds(fireRate);

        // Transition to idle or running
        TransitionToMovementState();

        // Wait for a slight pause
        yield return new WaitForSeconds(animationBreathTime);

        isAttacking = false;
    }
}
