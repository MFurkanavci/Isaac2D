using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public List<GameObject> roomPrefabs;
    public List<Room> roomsInFloor;
    public int totalFloor = 3;
    public List<int> totalRoomsInFloor = new List<int> { 18, 24, 15 };
    public GameObject bossRoomPrefab;
    private Room bossRoom;

    private Room firstRoom;

    void Start()
    {
        StartGame();
    }
    void Update()
    {
        if (PlayerEnterSameRoom())
        {
            Room _currentRoom = PlayerCurrentRoom();
            _currentRoom.ResetRoom();
        }
        if (TheDoorsOpen())
        {
            Room _currentRoom = PlayerCurrentRoom();
            OpenDoor openDoor = new OpenDoor();
            openDoor.OpenClosedDoor(_currentRoom.doorsInRoom);
        }
    }

    private bool TheDoorsOpen()
    {
        return false;
    }

    private Room PlayerCurrentRoom()
    {
        return null;
    }

    private bool PlayerEnterSameRoom()
    {
        return false;
    }

    void StartGame()
    {
        GenerateFloor();
        SpawnRoom();
        SelectFirstRoom();
        TransformPlayerToFirstRoom();
    }
    void GenerateFloor()
    {
        for (int _floor = 0; _floor < totalFloor; _floor++)
        {
            int _totalRoom = totalRoomsInFloor.Count;
            for (int i = 0; i < _totalRoom; i++)
            {
                Room _newRoom = GenerateNewRoom();
                roomsInFloor.Add(_newRoom);
            }
            if (_floor == totalFloor - 1)
            {
                GenerateBossRoom();
                roomsInFloor.Add(bossRoom);
            }
        }
    }
    void GenerateBossRoom()
    {
        bossRoom = new Room(bossRoomPrefab, "Boss");
        GenerateDoor generateDoor = new GenerateDoor();
        bossRoom.doorsInRoom = generateDoor.GenerateDoors(bossRoom);

        while (bossRoom.doorsInRoom.Count > 1)
        {
            bossRoom.doorsInRoom.RemoveAt(1);
        }

        GameObject doorObject = Instantiate(bossRoom.roomPrefab);
        BossRoomDoor bossRoomDoor = doorObject.AddComponent<BossRoomDoor>();
    }

    private Room GenerateNewRoom()
    {
        GameObject _prefab = SelectRandomRoom();
        string roomType = _prefab.name;
        Room room = new Room(_prefab, roomType);

        return room;
    }

    private GameObject SelectRandomRoom()
    {
        int index = UnityEngine.Random.Range(0, roomPrefabs.Count);
        return roomPrefabs[index];
    }

    private void TransformPlayerToFirstRoom()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = firstRoom.roomPrefab.transform.position;
    }

    private void SelectFirstRoom()
    {
        firstRoom = roomsInFloor[0];
    }

    private void SpawnRoom()
    {
        foreach (Room room in roomsInFloor)
        {
            Instantiate(room.roomPrefab);
        }
    }


}
