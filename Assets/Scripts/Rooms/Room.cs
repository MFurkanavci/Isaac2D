using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Room : MonoBehaviour
{
    public RoomType roomType;

    public void SetRoomType(RoomType roomType)
    {
        this.roomType = roomType;
    }

    void OnEnable()
    {
    }

    void OnDisable()
    {
        
    }
}