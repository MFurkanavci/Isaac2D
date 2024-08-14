using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance { get; private set; }
    public GameObject[] roomPrefabs;

    private Dictionary<DoorType, Room> roomDictionary;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeRooms();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void InitializeRooms()
    {
        roomDictionary = new Dictionary<DoorType, Room>();
        DoorType[] doorTypes = (DoorType[])System.Enum.GetValues(typeof(DoorType));
        for (int i = 0; i < roomPrefabs.Length; i++)
        {
            if (i < doorTypes.Length)
            {
                Room room = Instantiate(roomPrefabs[i]).GetComponent<Room>();
                roomDictionary.Add(doorTypes[i], room);
            }
        }
    }
    public Room GetRoomByDoorType(DoorType doorType)
    {
        return roomDictionary.ContainsKey(doorType) ? roomDictionary[doorType] : null;
    }
}
