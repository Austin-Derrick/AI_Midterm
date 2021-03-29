using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendState : BaseState
{
    public Building buildingToDefend;
    public DefendState(Unit unit) : base(unit.gameObject)
    {

    }

    public override Type Tick()
    {
        return typeof(DefendState);
    }
}
