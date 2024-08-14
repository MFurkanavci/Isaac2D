using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum DoorType
{
    TreasureRoom,
    BossRoom,
    EventRoom,
    MarketRoom,
    MiniBossRoom,
    ClassicRoom,
    ChallengeRoom
}
public class DoorScript : MonoBehaviour
{
    public DoorType doorType;
    public bool isLocked = true;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Player player) && !isLocked)
        {

            Room nextRoom = RoomManager.Instance.GetRoomByDoorType(doorType);
            Debug.Log("you are going to" + nextRoom);
            if (nextRoom == null) return;
            Debug.Log("you are going to" + nextRoom);


        }
    }
}
