using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemPart : MonoBehaviour

{
    public Vector2 direction;
    public float bulletSpeed = 1f;
    public float bulletDamping = 3;
    public int bulletDamage = 2;
    void Start()
    {
        StartCoroutine(DestroyBullet());
    }
    void Update()
    {
        transform.Translate(direction * bulletSpeed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(bulletDamage);
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bulletDamping);
        Destroy(gameObject);
    }
}
