using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Room: MonoBehaviour
{
    public RoomType roomType;
    public List<Door> doors = new List<Door>();

    public GameObject environment;

    public void SetRoomType(RoomType roomType)
    {
        this.roomType = roomType;
    }
}