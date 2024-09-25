using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance;
    private List<Door> doors = new List<Door>();

    public int maxDoorAmount;

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

    public void GenerateDoors()
    {
        ClearDoors();
        int doorCount = Random.Range(2, maxDoorAmount + 1);
        GenerateDoors(doorCount);
        SetDoors();
    }

    void OnDisable()
    {
        // Add any necessary cleanup code here
    }

    public void ClearDoors()
    {
        doors.Clear();
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy) child.gameObject.SetActive(false);
        }
    }

    public void GenerateDoors(int doorCount)
    {
        for (int i = 0; i < doorCount && i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void SetDoors()
    {
        foreach (Door door in transform.GetComponentsInChildren<Door>())
        {
            if (!door.gameObject.activeInHierarchy) continue;
            doors.Add(door);
            door.SetDoorType(RoomManager.Instance.GetRoomType());
        }

        SetDoorSprites();
    }

    public List<Door> GetDoors()
    {
        return doors;
    }

    public Door GetDoor(int index)
    {
        if (index < 0 || index >= doors.Count)
        {
            Debug.LogWarning("Door index out of range: " + index);
            return null;
        }

        return doors[index];
    }

    public void OpenDoors()
    {
        foreach (Door door in doors)
        {
            door.OpenDoor();
        }

        HandleManager();
    }
    public void HandleManager()
    {
        EnemySpawner.Instance.totalEnemies += EnemySpawner.Instance.totalIncrease;
    }

    public void HandleExperience()
    {
        Player.Instance.tempExperience = RoomManager.Instance.GetRoomExperience();
        Player.Instance.AddExperience(Player.Instance.tempExperience);
    }

    public void CloseDoors()
    {
        foreach (Door door in doors)
        {
            door.CloseDoor();
        }
    }

    public void SetDoorSprites()
    {
        foreach (Door door in doors)
        {
            Sprite doorSprite = RoomManager.Instance.GetSprite(door.GetDoorType());
            if (doorSprite != null)
            {
                door.SetSprite(doorSprite);
            }
            else
            {
                Debug.LogWarning("Sprite not found for door type: " + door.GetDoorType());
            }
        }
    }
}
