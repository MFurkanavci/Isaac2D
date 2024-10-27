using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testslash : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
         if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(1);
            ObjectPool.Instance.ReturnToPool(gameObject, gameObject.tag);
        }

    }
}
