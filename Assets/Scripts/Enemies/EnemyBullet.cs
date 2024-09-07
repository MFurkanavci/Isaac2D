using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletLifetime = 2f;

    public Vector2 direction;

    private void OnEnable()
    {
        transform.LookAt(direction,Vector2.down);
        StartCoroutine(ReturnToPool());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    void Update()
    {
        transform.position += (Vector3)direction * bulletSpeed * Time.deltaTime;
    }

    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(bulletLifetime);
        ObjectPool.Instance.ReturnToPool(gameObject, gameObject.tag);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(1);
            ObjectPool.Instance.ReturnToPool(gameObject, gameObject.tag);
        }
    }
}
