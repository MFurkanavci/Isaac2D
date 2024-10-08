using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWolf : Enemy
{
    [Header("Wolf Enemy")]
    [SerializeField] private float jumpDistance;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float jumpCooldownTime;
    private Vector2 jumpDirection;

    protected override void Start()
    {
        base.Start();
        float random = Random.Range(-0.5f, 0.5f);
        jumpCooldown = jumpCooldown * (1 + random);
    }

    protected override void Update()
    {
        base.Update();


        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= jumpDistance && Time.time >= jumpCooldownTime)
        {
            JumpDirection();
            Jump();
        }
    }

    protected override void Move()
    {

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > jumpDistance)
        {
            base.Move();
        }

    }

    private void Jump()
    {
        float elapsedTime = 0;
        rb.velocity = Vector2.Lerp(jumpDirection * jumpSpeed, Vector2.zero, elapsedTime / 10f);
        jumpCooldownTime = Time.time + jumpCooldown;
        elapsedTime += Time.deltaTime;
    }

    private void JumpDirection()
    {
        jumpDirection = (player.position - transform.position).normalized;
    }
}
