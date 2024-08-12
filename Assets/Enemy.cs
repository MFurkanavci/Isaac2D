using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy: MonoBehaviour
{
    [Header("Loot Pooling")]
    public ObjectPool objectPool;
    public string lootTag = "Loot";

    [Header("Enemy Components")]
    protected Rigidbody2D rb;
    protected Transform player;
    protected Vector2 moveDirection;

    [Header("Enemy Stats")]
    public float moveSpeed;
    public int health;
    public int damage;

    [Header("Loot")]
    public GameObject lootDrop;
    public float lootDropChance;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        moveDirection = (player.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (Random.value <= lootDropChance)
        {
            GameObject loot = objectPool.GetFromPool(lootTag, transform.position, Quaternion.identity);
            loot.GetComponent<Loot>().OnSpawn();
        }
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(damage);
            Die();
        }
    }

    public virtual void OnSpawn()
    {
        health = 100;
    }
}
