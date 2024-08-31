using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Loot Pooling")]
    public string lootTag = "Loot";

    public float lootDropChance;

    [Header("Enemy Components")]
    protected Rigidbody2D rb;
    protected Transform player;
    protected Vector2 moveDirection;

    [Header("Enemy Stats")]
    public float moveSpeed;
    public int health;
    public int maxHealth;
    public int damage;

    private bool mmHit = false;

    SpriteRenderer spriteRenderer;

    protected virtual void OnEnable()
    {
        health = maxHealth;
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        moveDirection = (player.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = !(moveDirection.x > 0);
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
        EnemySpawner.Instance.RemoveEnemy(gameObject);
        ObjectPool.Instance.ReturnToPool(gameObject, gameObject.tag);
        LootDrop();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(damage);
        }
    }
    protected virtual void LootDrop()
    {
        if (Random.value <= lootDropChance)
        {
            GameObject loot = ObjectPool.Instance.GetFromPool(lootTag, transform.position, Quaternion.identity);
        }
    }

    public virtual void OnSpawn()
    {
        health = 1;
    }
    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    public virtual void Clean()
    {
        Die();
    }
    public void ApplyEffect(MissileSpells missile)
    {
        switch (missile.missileType)
        {
            case MissileType.Fireball:
                DOT(missile.damage, missile.DOTDuration);
                break;
            case MissileType.IceBall:
                SlowDown(missile.slowDuration);
                break;
            case MissileType.LightningBall:
                Electrocute(missile.damage, missile.DOTDuration, missile.radius);
                break;
            case MissileType.MagicMissile:
                DealDamageToClosestEnemy(missile.damage, missile.radius, missile.DOTDuration);
                break;
        }

        TakeDamage(missile.damage);
    }
    public virtual void SlowDown(float duration)
    {
        StartCoroutine(SlowDownCoroutine(duration));
    }

    private IEnumerator SlowDownCoroutine(float duration)
    {
        moveSpeed /= 2;
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(duration);
        moveSpeed *= 2;
        spriteRenderer.color = Color.clear;
    }

    public virtual void DOT(int damage, float duration)
    {
        StartCoroutine(DOTCoroutine(damage, duration));
    }

    private IEnumerator DOTCoroutine(int damage, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            spriteRenderer.color = Color.red;
            TakeDamage(damage);
            yield return new WaitForSeconds(1);
        }

        spriteRenderer.color = Color.clear;
    }

    public virtual void Stun(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    public virtual void Freeze(float duration)
    {
        StartCoroutine(StunCoroutine(duration));
    }

    private IEnumerator StunCoroutine(float duration)
    {
        float tempSpeed = moveSpeed;
        moveSpeed = 0;
        yield return new WaitForSeconds(duration);
        moveSpeed = tempSpeed;
    }

    public virtual void Electrocute(int damage, float duration, float radius)
    {
        StartCoroutine(ElectrocuteCoroutine(damage, duration, radius));
    }

    private IEnumerator ElectrocuteCoroutine(int damage, float duration, float radius)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Enemy enemy) && enemy != this)
            {
                enemy.spriteRenderer.color = Color.yellow;
                enemy.TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(duration);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Enemy enemy) && enemy != this)
            {
                enemy.spriteRenderer.color = Color.clear;
            }
        }
    }

    public virtual void DealDamageToClosestEnemy(int damage, float radius, float duration)
    {
        if (mmHit) return;

        spriteRenderer.color = Color.magenta;

        mmHit = true;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        float minDistance = Mathf.Infinity;
        Enemy closestEnemy = null;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Enemy enemy) && enemy != this)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }
        if (closestEnemy != null && !closestEnemy.mmHit)
        {
            closestEnemy.TakeDamage(damage);
            closestEnemy.DealDamageToClosestEnemy(damage, radius, duration);
        }

        StartCoroutine(MMCoroutine(duration));
    }

    private IEnumerator MMCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        mmHit = false;
        spriteRenderer.color = Color.clear;
    }
}
