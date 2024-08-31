using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EventAction : MonoBehaviour
{

    [Header("Event Action")]
    public bool isEventDone = false;
    public bool isTimedEvent;
    public bool isTimeOver;
    public float eventCountDown;

    [Header("Childs")]
    public List<GameObject> childs = new();

    protected virtual void OnEnable()
    {
        foreach (Transform item in transform)
        {
            childs.Add(item.gameObject);


            if (!item.gameObject.TryGetComponent<ColliderEventListener>(out _))
            {
                item.gameObject.AddComponent<ColliderEventListener>().ParentEventAction = this;
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out _))
        {
            TriggerEvent();
        }
    }

    protected virtual void TriggerEvent()
    {
        
    }
    public virtual void EventComplete()
    {
        DoorManager.Instance.OpenDoors();
    }
    public void OnChildTrigger(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out _))
        {
            TriggerEvent();
        }
    }
}

public class ColliderEventListener : MonoBehaviour
{
    public EventAction ParentEventAction;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ParentEventAction?.OnChildTrigger(other);
    }
}


