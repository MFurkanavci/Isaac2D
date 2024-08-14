using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door
{
    public bool isLocked;
    public Room connectionRoom;
    public Door()
    {
        this.isLocked = true;
    }

    public void openDoor()
    {
        this.isLocked = false;
    }
}
