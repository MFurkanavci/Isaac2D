using System.Collections;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    [Header("Skeleton Enemy")]
    [SerializeField] float reConstructTime;
    [SerializeField] GameObject skeleton;
    [SerializeField] Collider2D skeletonCollider;
    [SerializeField] GameObject bonePile;
    [SerializeField] Collider2D bonePillCollider;



    private bool inSecondPhase = false;

    private float moveSpeedFirst;


    protected override void Start()
    {
        skeleton.SetActive(true);
        bonePile.SetActive(false);
        bonePillCollider.enabled = false;
        skeletonCollider.enabled = true;
        base.Start();
        moveSpeedFirst = moveSpeed;


    }
    protected override void Move()
    {
        if (inSecondPhase)
        {
            moveSpeed = 0;
        }

        base.Move();
    }

    public override void TakeDamage(int damage)
    {
        if(health - damage <= 0 && !inSecondPhase)
        {
            health = 1;
        }
        if(inSecondPhase)
        {
            health = 0;
        }
        if(health <= maxHealth / 2 + 1 && !inSecondPhase)
        {
            inSecondPhase = true;
            StartCoroutine(ReConstruct());
        }

        if (!inSecondPhase)
        {
            base.TakeDamage(damage);
        }
        else
        {
            health -= damage;
            if (health <= 0)
            {
                base.Die();
            }
        }
    }
    IEnumerator ReConstruct()
    {
        inSecondPhase = true;
        skeleton.SetActive(false);
        bonePile.SetActive(true);
        bonePillCollider.enabled = true;
        skeletonCollider.enabled = false;
        yield return new WaitForSeconds(reConstructTime);
        inSecondPhase = false;
        skeleton.SetActive(true);
        bonePile.SetActive(false);
        bonePillCollider.enabled = false;
        skeletonCollider.enabled = true;
        moveSpeed = moveSpeedFirst;
        health = maxHealth;
    }
}
