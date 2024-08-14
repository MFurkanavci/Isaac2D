using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public GameObject roomPrefab;
    public List<Door> doorsInRoom;
    public string roomType;
    public Room(GameObject roomPrefab,string roomType)
    {
        this.roomPrefab = roomPrefab;
        this.roomType = roomType;
        doorsInRoom = new List<Door>();
        GenerateDoorsInRoom();
    }

    public void GenerateDoorsInRoom()
    {
        GenerateDoor generateDoor = new GenerateDoor();
        doorsInRoom = generateDoor.GenerateDoors(this);
    }
    public void ResetRoom()
    {
        doorsInRoom.Clear();
        GenerateDoorsInRoom();
    }
}
