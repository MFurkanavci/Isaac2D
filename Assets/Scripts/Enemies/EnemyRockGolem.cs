using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRockGolem : Enemy
{
    [Header("RockGolem")]
    [SerializeField] GameObject golemPart;
    public float bulletSpeed = 5f;


    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }
    protected override void Die()
    {
        LastShoot();
        base.Die();
    }

    private void LastShoot()
    {
        ShootBullet(new Vector2(1, 1));
        ShootBullet(new Vector2(-1, -1));
        ShootBullet(new Vector2(-1, 1));
        ShootBullet(new Vector2(1, -1));
    }

    void ShootBullet(Vector2 direction)
    {
        GameObject golemBullet = Instantiate(golemPart, transform.position, Quaternion.identity) as GameObject;
        RockGolemPart golemBulletScript = golemBullet.GetComponent<RockGolemPart>();
        golemBulletScript.direction =direction.normalized;
        golemBulletScript.bulletSpeed = bulletSpeed;
    }
}
