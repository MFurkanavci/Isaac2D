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
        StartCoroutine(ReturnToPool());
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        // Reset bullet position
        transform.position = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void Update()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }

    private IEnumerator ReturnToPool()
    {
        yield return new WaitForSeconds(bulletLifetime);
        gameObject.SetActive(false);
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
