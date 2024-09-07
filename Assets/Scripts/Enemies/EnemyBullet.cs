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
        SetDirection();
        StartCoroutine(ReturnToPool());
    }

    public void SetDirection()
    {
        //set LookAt to player
        Vector2 lookDir = (Vector2)Player.Instance.transform.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
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
