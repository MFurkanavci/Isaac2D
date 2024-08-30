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
