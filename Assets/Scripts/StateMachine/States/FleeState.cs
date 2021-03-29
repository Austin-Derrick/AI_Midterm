using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeState : BaseState
{
    public GameObject Target;
    public FleeState(Unit unit) : base(unit.gameObject)
    {

    }

    public override Type Tick()
    {
        return typeof(FleeState);
    }
}
