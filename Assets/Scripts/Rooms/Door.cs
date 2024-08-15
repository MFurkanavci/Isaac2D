using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Door: MonoBehaviour
{
    public RoomType doorType;
    public Sprite sprite;

    private bool isLocked = true;

    public void OpenDoor()
    {
        isLocked = false;
    }

    public void CloseDoor()
    {
        isLocked = true;
    }

    public RoomType GetDoorType()
    {
        return doorType;
    }

    public void SetDoorType(RoomType _type)
    {
        doorType = _type;
    }

    public void SetSprite(Sprite _sprite)
    {
        sprite = _sprite;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
    }

    void OnDisable()
    {
        CloseDoor();
        SetSprite(null);
    }
}
