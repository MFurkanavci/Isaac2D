using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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
