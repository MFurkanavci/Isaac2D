using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private HeroSO hero;
    //2d player shooting
    [Header("Player Components")]
    private Rigidbody2D rb;
    public GameObject slash;

    [Header("Shooting")]
    public float fireRate = 0.5f;
    public float bulletLifetime = 2f;
    public float bulletSpeed = 10f;
    public float fireTimer;

    [Header("Bullet Pooling")]
    public string bulletTag = "Bullet";

    Vector2 mousePosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void InitializePlayerShooting(HeroSO hero)
    {
        this.hero = hero;
        fireRate = hero.attackSpeed;
        bulletSpeed = hero.bulletSpeed;
        slash = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;

    }

    void FixedUpdate()
    {
        // Shoot when left mouse button is pressed or held down(be sure both conditions are the same)

        //TODO:if they see us rollin, they hatin (we cant shoot)
        if (Input.GetMouseButton(0) && hero != null)
        {
            if (fireTimer <= 0)
            {
                Shoot();
                fireTimer = fireRate;
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

    void ShootingAnimationSpeed()
    {
        float speed = 1 / fireRate;
        SetFloatAnim("animSpeed", speed);
    }

    public void Shoot()
    {
        // Get mouse position in world space
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction to mouse position
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        gameObject.GetComponentInChildren<SpriteRenderer>().flipX = direction.x < 0;

        slash.SetActive(true);
        /*  
            slash.GetComponent<Slash>().SetPositionAndDirection(direction);
            slash.GetComponent<Slash>().InitializeSlash(hero.attackDamage, hero.attackRange, hero.attackSpeed);
        */

        CycleAttacks();

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

    bool firstAttack = false;

    public void CycleAttacks()
    {
        if (!firstAttack)
        {
            TriggerAnim("Attack1");
            firstAttack = true;
        }
        else
        {
            TriggerAnim("Attack2");
            firstAttack = false;
        }

        ShootingAnimationSpeed();
    }

    public void SpecialAttack()
    {
        TriggerAnim("SpecialAttack");
    }

    public void UltimateAttack()
    {
        TriggerAnim("UltimateAttack");
    }
}
