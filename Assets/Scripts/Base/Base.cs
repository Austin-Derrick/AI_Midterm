using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Team))]

public class Base : MonoBehaviour
{
    [SerializeField]
    public List<Destructable> destructable =  new List<Destructable>();

    public List<GameObject> buildings = new List<GameObject>();
    public List<GameObject> enemyBuildings = new List<GameObject>();

    private const int MAX_BUILDINGS = 4;
    public Team.side team;

    int destroyedBuildings = 0;

    private void Update()
    {
        foreach (var buidling in buildings)
        {
            if (buidling.GetComponent<Building>().state == Building.State.Destroyed)
            {
                destroyedBuildings++;
            }
        }

        if (destroyedBuildings == 4)
        {
            Debug.Log(team + " Lost ");
        }
        else
        {
            destroyedBuildings = 0;
        }
    }

    private void OnEnable()
    {
        List<GameObject> allBuildings = GameObject.FindGameObjectsWithTag("Building").ToList();

        foreach (var building in allBuildings)
        {
            if (building.GetComponent<Team>().team == team)
            {
                buildings.Add(building);
            }
            else
            {
                enemyBuildings.Add(building);
            }
        }

        foreach (Destructable building in destructable)
        {
            building.OnDestroyObject += RemoveBuilding;
        }
        Debug.Log("Set");
    }

    public void RemoveBuilding(GameObject building)
    {
        if (building.GetComponent<Team>().team == team)
        {
            buildings.Remove(building);
        }
        else
        {
            enemyBuildings.Remove(building);
        }

        Debug.Log($"Destroyed : { building.GetComponent<Team>().team}'s {building.name}");
    }
}