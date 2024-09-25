using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            DoorManager.Instance.GenerateDoors();
            DoorManager.Instance.OpenDoors();
            Destroy(gameObject);
        }
    }
}
