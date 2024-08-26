using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImp : Enemy
{


    [Header("Shooter Enemy")]
    public float fireRate;
    private float nextFireTime = 2;
    private float teleportCooldown = 5f;
    public float minX, maxX, minY, maxY;
    private float nextTeleportTime;

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
            if (Time.time >= nextTeleportTime)
            {
                Teleport();
                nextTeleportTime = Time.time + teleportCooldown;
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = ObjectPool.Instance.GetFromPool("EnemyBullet", transform.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().direction = (player.position - transform.position).normalized;
        nextFireTime = Time.time + fireRate;
    }
    void Teleport()
    {
        float newX = Random.Range(minX, maxX);
        float newY = Random.Range(minY, maxY);
        transform.position = new Vector2(newX, newY);
    }
}
