using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEvent : EventAction
{
    protected override void TriggerEvent()
    {
        print("Sandik H.O");
        base.TriggerEvent();
        base.EventComplete();
    }
}
