using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeEvent : EventAction
{
    protected override void TriggerEvent()
    {
        print("Pic H.O");
        base.TriggerEvent();
        base.EventComplete();

    }
}
