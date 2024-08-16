using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //2d player shooting
    [Header("Player Components")]
    private Rigidbody2D rb;

    [Header("Shooting")]
    public float fireRate = 0.5f;
    private float fireTimer;
    
    [Header("Bullet Pooling")]
    public string bulletTag = "Bullet";

    Vector2 mousePosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fireTimer = fireRate;
    }

    void FixedUpdate()
    {
        // Shoot when left mouse button is pressed or held down(be sure both conditions are the same)
        if (Input.GetMouseButton(0))
        {
            if (fireTimer <= 0)
            {
                Shoot();
                fireTimer = fireRate;
            }
            else
            {
                fireTimer -= Time.deltaTime;
            }
        }
    }

    private void Shoot()
    {
        // Get mouse position in world space
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction to mouse position
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

        // Get bullet object from the pool
        GameObject bullet = ObjectPool.Instance.GetFromPool(bulletTag, transform.position, Quaternion.identity);

        // Set bullet direction
        bullet.GetComponent<Bullet>().direction = direction;
    }
}
