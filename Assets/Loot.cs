using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    void OnEnable()
    {
        //play spawn animation
        //play spawn sound
    }

    void OnDisable()
    {
        //play despawn animation
        //play despawn sound
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * 50);
    }

    public void OnPickup()
    {
        //play pickup animation
        //play pickup sound
        //add loot to player inventory
        //destroy the loot
        ObjectPool.Instance.ReturnToPool(gameObject, "Loot");

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            OnPickup();
            player.AddMoney(1);
        }
    }
}
