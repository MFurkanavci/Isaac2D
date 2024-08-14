using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomDoor : MonoBehaviour
{
    private NextFloor nextFloor;

    void Start()
    {
        nextFloor = new NextFloor();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            GoToNextFloor();
        }
    }

    private void GoToNextFloor()
    {
        nextFloor.GoToNextFloor();
    }
}
