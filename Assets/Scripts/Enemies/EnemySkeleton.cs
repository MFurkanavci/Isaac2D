using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    [Header("Skeleton Enemy")]
    [SerializeField] float deathTime;
    [SerializeField] GameObject skeleton;
    [SerializeField] Collider2D skeletonCollider;
    [SerializeField] GameObject bonePill;
    [SerializeField] Collider2D bonePillCollider;



    private bool isDead = false;

    private float moveSpeedFirst;


    protected override void Start()
    {
        skeleton.SetActive(true);
        bonePill.SetActive(false);
        bonePillCollider.enabled = false;
        skeletonCollider.enabled = true;
        base.Start();
        moveSpeedFirst = moveSpeed;


    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void Move()
    {
        base.Move();
    }

    public override void TakeDamage(int damage)
    {
        if (isDead)
        {
            StopAllCoroutines();
        }
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        if (isDead)
        {
            base.Die();
        }
        if (!isDead)
        {
            moveSpeed = 0;
            isDead = true;
            skeleton.SetActive(false);
            bonePill.SetActive(true);
            bonePillCollider.enabled = true;
            skeletonCollider.enabled = false;
            StartCoroutine(ReviveCountdown());
        }
    }

    private IEnumerator ReviveCountdown()
    {
        yield return new WaitForSeconds(deathTime);

        if (isDead)
        {
            skeleton.SetActive(true);
            bonePill.SetActive(false);
            health = maxHealth;
            bonePillCollider.enabled = false;
            skeletonCollider.enabled = true;
            
            moveSpeed = moveSpeedFirst;
            isDead = false;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
