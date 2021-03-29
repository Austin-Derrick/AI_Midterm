using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInstatiation
{
    int TotalUnits { get; set; }
    int AvailableUnits { get; set; }
    int UNIT_LIMIT { get; }

    void InstatiateUnit(Vector3 mousePos);
    void CheckRequisite();
}

public class GameSettings : IInstatiation
{
    public int TotalUnits { get; set; }
    public int AvailableUnits { get; set; }
    public int UNIT_LIMIT { get; }

    void IInstatiation.InstatiateUnit(Vector3 mousePos)
    {

    }

    void IInstatiation.CheckRequisite()
    {

    }
}