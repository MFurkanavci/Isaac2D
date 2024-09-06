using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;

    public Vector2 direction;

    private void OnEnable()
    {
        StartCoroutine(ReturnToPool());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        transform.position = Vector2.zero;
    }

    public void InitializeBullet(int damage,float speed,float range)
    {
        this.damage = damage;
        bulletLifetime = range;
        bulletSpeed = speed;
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    void Update()
    {
        transform.position += (Vector3)direction * bulletSpeed * Time.deltaTime;
    }

    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(bulletLifetime);
        ObjectPool.Instance.ReturnToPool(gameObject,gameObject.tag);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(1);
            ObjectPool.Instance.ReturnToPool(gameObject, gameObject.tag);
        }
    }
}
