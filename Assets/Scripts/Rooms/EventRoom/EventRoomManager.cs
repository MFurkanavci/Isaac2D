using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventRoomManager : MonoBehaviour
{
    public static EventRoomManager Instance { get; private set; }
    public List<GameEvents> events = new();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

    }



    void Start()
    {

    }

    public void GenerateEvent()
    {
        int _random = UnityEngine.Random.Range(0, events.Count);
        GameObject currentEvent = Instantiate(events[_random].eventGameObject);
        currentEvent.transform.position = new Vector3(0, 0, 0);
        Transform _transform = RoomManager.Instance.GetCurrentRoom().transform;
        currentEvent.transform.parent = _transform;
    }

}
