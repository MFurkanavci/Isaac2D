using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance;
    List<Door> doors = new List<Door>();

    public int doorAmount = 2;
    public void SetDoors()
    {
        doors.Clear();
        foreach(Door door in transform.GetComponentsInChildren<Door>())
        {
            if(!door.gameObject.activeInHierarchy) continue;
            doors.Add(door);
            door.SetDoorType(RoomManager.Instance.GetRoomType());
            SetDoorSprites();
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        
        GenerateDoors(UnityEngine.Random.Range(2, doorAmount+1));
        SetDoors();
    }

    void OnDisable()
    {

    }

    public void GenerateDoors(int doorAmount)
    {
        for(int i = 0; i < doorAmount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void OpenDoors()
    {
        foreach(Door door in doors)
        {
            door.OpenDoor();
        }
    }

    public void CloseDoors()
    {
        foreach(Door door in doors)
        {
            door.CloseDoor();
        }
    }

    public void SetDoorSprites()
    {
        foreach(Door door in doors)
        {
            door.SetSprite(RoomManager.Instance.GetSprite(door.GetDoorType()));
        }
    }
}
