using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntState : BaseState
{
    public GameObject Target;
    public TauntState(Unit unit) : base(unit.gameObject)
    {

    }

    public override Type Tick()
    {
        return typeof(TauntState);
    }
}
