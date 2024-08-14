using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDoor : MonoBehaviour
{
    public int minDoorQuantity = 2;
    public int maxDoorQuantity = 4;
    
    
    public List<Door> GenerateDoors(Room room)
    {
        List<Door> _doorList = new List<Door>();
        int _doorCount = SelectRandomRoom();
        for (int i = 0; i < _doorCount; i++)
        {
            Door _newDoor = new Door();
            _newDoor.connectionRoom = null;
            _doorList.Add(_newDoor);
        }
        return _doorList;
    }

    int SelectRandomRoom()
    {
        return UnityEngine.Random.Range(minDoorQuantity, maxDoorQuantity+1);
    }
}
