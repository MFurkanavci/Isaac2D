using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public static PlayerShooting Instance;
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
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
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
        if (Input.GetMouseButton(0) && hero != null)
        {
            if (fireTimer <= 0)
            {
                Shoot();
                CycleAttacks();
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
        //we need to set the anim speed match with the fire rate of the hero, both attack1 and attack2 animations should be the same length and speed with the fire rate
        //first we need to calculate the speed of the animations
        //speed = 1 / fireRate
        //then we need to set the speed of the animations
        //anim.speed = speed
        //we need to set the speed of the animations in the cycle attacks method

        float speed = 1 / fireRate;
        SetFloatAnim("animSpeed", speed);
    }

    public void Shoot()
    {
        // Get mouse position in world space
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction to mouse position
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        // Get bullet object from the pool
        //GameObject bullet = ObjectPool.Instance.GetFromPool(bulletTag, transform.position, Quaternion.identity);
        slash.SetActive(true);
        slash.GetComponent<Slash>().direction = direction;
        slash.GetComponent<Slash>().InitializeSlash(hero.attackDamage, hero.attackRange, bulletSpeed);

        // Set bullet direction



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
