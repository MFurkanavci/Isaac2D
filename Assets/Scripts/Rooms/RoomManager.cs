using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;
    public TextMeshProUGUI keyAmountText;
    public Room currentRoom;
    GameObject room;
    public RoomType currentRoomType;
    private int roomExperience;
    private int keyAmount = 15;

    public GameObject cleaner;

    [System.Serializable]
    public struct RoomData
    {
        public RoomType roomType;
        public int baseWeight;
        public int currentWeight;
        public int maxWeight;
        public int weightIncrease;
        public bool canSpawnMultiple;
        public Sprite roomSprite;
        public GameObject roomPrefab;
    }

    public List<RoomData> roomDataList = new List<RoomData>();

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

    void Start()
    {
        keyAmountText.text = keyAmount.ToString();
    }
    public void UseKey()
    {
        keyAmount -= 1;
        keyAmountText.text = keyAmount.ToString();
    }
    public void IncreaseKey(int key)
    {
        keyAmount += key;
        keyAmountText.text = keyAmount.ToString();
    }

    public Room GetCurrentRoom()
    {
        return currentRoom;
    }

    public void EnterRoom(Room room)
    {

        currentRoom = room;
        currentRoomType = room.roomType;

        ResetRoomWeights();

        DoorManager.Instance.GenerateDoors();

        roomExperience = EnemySpawner.Instance.totalEnemies;

    }

    public int GetRoomExperience()
    {
        return roomExperience;
    }

    public void ExitRoom()
    {
        Cleaner();
        Destroy(currentRoom.gameObject);
        currentRoom = null;
        room = null;
    }

    public void Cleaner()
    {
        cleaner.SetActive(true);
    }

    public void GenerateRoom(RoomType roomType)
    {
        RoomType newRoomType = roomType;

        foreach (RoomData data in roomDataList)
        {
            if (data.roomType == newRoomType)
            {
                GameObject room = Instantiate(data.roomPrefab, transform.position, Quaternion.identity);
                Room roomComponent = room.GetComponent<Room>();
                roomComponent.SetRoomType(newRoomType);
                this.room = room;
                EnterRoom(roomComponent);
            }
        }
    }

    public RoomType GetRoomType()
    {
        if (roomDataList.Count == 0)
        {
            Debug.LogWarning("No rooms configured in the RoomManager.");
            return RoomType.StartRoom;
        }
        int totalWeight = 0;
        foreach (RoomData data in roomDataList)
        {
            totalWeight += data.currentWeight;
        }

        int random = Random.Range(0, totalWeight);
        int currentWeight = 0;
        foreach (RoomData data in roomDataList)
        {
            currentWeight += data.currentWeight;
            if (random < currentWeight)
            {
                if (data.canSpawnMultiple || !data.roomType.Equals(currentRoomType))
                {
                    return data.roomType;
                }
            }
        }

        return RoomType.StartRoom;
    }

    private void ResetRoomWeights()
    {
        List<RoomData> updatedRoomDataList = new List<RoomData>();

        foreach (RoomData data in roomDataList)
        {
            RoomData updatedData = data;

            if (data.roomType.Equals(currentRoomType))
            {
                // Reset weight to original value for the room type
                updatedData.currentWeight = updatedData.baseWeight;
            }
            else
            {
                updatedData.currentWeight = Mathf.Min(updatedData.currentWeight + updatedData.weightIncrease, updatedData.maxWeight);
            }

            updatedRoomDataList.Add(updatedData);
        }

        roomDataList = updatedRoomDataList;
    }

    public Sprite GetSprite(RoomType roomType)
    {
        foreach (RoomData data in roomDataList)
        {
            if (data.roomType == roomType)
            {
                return data.roomSprite;
            }
        }

        Debug.LogWarning("Room sprite not found for type: " + roomType);
        return null;
    }
}
