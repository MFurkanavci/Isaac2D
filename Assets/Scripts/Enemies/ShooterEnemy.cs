using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    [Header("Shooter Enemy")]
    public float fireRate;
    private float nextFireTime = -2;

    protected override void Start()
    {
        float random = Random.Range(-0.5f, 0.5f);
        fireRate += random;
        base.Start();
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
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    void FixedUpdate()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPool.Instance.GetFromPool("EnemyBullet", transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().direction = (player.position - transform.position).normalized;
        nextFireTime = Time.time + fireRate;
    }
    public override void Freeze(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    public override void Stun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    IEnumerator StunCoroutine(float duration)
    {
        float tempSpeed = moveSpeed;
        float tempFireRate = fireRate;
        moveSpeed = 0;
        fireRate = 0;
        yield return new WaitForSeconds(duration);
        moveSpeed = tempSpeed;
        fireRate = tempFireRate;
    }
}