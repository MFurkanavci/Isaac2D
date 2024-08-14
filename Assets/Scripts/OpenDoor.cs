using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor
{
    public void OpenClosedDoor(List<Door> doors)
    {
        foreach (Door door in doors)
        {
            door.openDoor();
        }
    } 
}
