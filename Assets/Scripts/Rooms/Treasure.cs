using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public int moneyAmount;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.money += moneyAmount;
            
            DoorManager.Instance.OpenDoors();
            
            Destroy(gameObject);

        }
    }
}
