using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameEvents
{
    public string eventName;
    public GameObject eventGameObject;
    public int eventRarity;
    public bool isOneTimeEvent;
    public bool isLastEvent;
}
