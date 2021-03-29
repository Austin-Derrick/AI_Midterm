using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    public GameObject Target;
    public ChaseState(Unit unit) : base(unit.gameObject)
    {

    }

    public override Type Tick()
    {
        return typeof(ChaseState);
    }
}
