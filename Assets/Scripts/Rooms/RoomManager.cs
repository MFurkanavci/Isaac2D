using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    Dictionary<RoomType, int> roomChances = new Dictionary<RoomType, int>();
    public Room currentRoom;
    public RoomType currentRoomType;

    [System.Serializable]
    public struct Rooms
    {
        public RoomType roomType;
        public int weight;
        public Sprite roomSprite;
    }

    public List<Rooms> chances = new List<Rooms>();

    public static event System.Action OnRoomChange;

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

    public void Start()
    {
        foreach(Rooms chance in chances)
        {
            roomChances.Add(chance.roomType, chance.weight);
        }

        GenerateRoom();
    }

    public void ChangeChances(RoomType roomType, int chance)
    {
        roomChances[roomType] = chance;
    }
    public void EnterRoom(Room room)
    {
        currentRoom = room;
        currentRoomType = room.roomType;
    }

    public void ExitRoom()
    {
        currentRoom = null;
    }
    public void GenerateRoom()
    {
        RoomType roomType = GetRoomType();
        Room room = new Room();
        room.SetRoomType(roomType);

        print("Selected Room: " + room.roomType);
    }

    public RoomType GetRoomType()
    {
        //pick a room type based on their weight
        int totalWeight = 0;
        foreach (Rooms chance in chances)
        {
            totalWeight += chance.weight;
        }
        int random = Random.Range(0, totalWeight);
        int currentWeight = 0;
        foreach (Rooms chance in chances)
        {
            currentWeight += chance.weight;
            if (random <= currentWeight)
            {
                return chance.roomType;
            }
        }

        return RoomType.StartRoom;
    }

    public Sprite GetSprite(RoomType _type)
    {
        foreach (Rooms chance in chances)
        {
            if(chance.roomType == _type)
            {
                return chance.roomSprite;
            }
        }
        return null;
    }
    
}
