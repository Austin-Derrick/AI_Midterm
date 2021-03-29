using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    public delegate void DestroyBuilding(GameObject building);
    public event DestroyBuilding OnDestroyObject;
    Team side;


    // Start is called before the first frame update
    void Start()
    {
        side = GetComponent<Team>();
    }
}