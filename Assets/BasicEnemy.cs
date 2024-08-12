using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    protected override void Start()
    {
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
}
