using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class SearchState : BaseState
{
    NavMeshAgent navMeshAgent;
    public Unit unit;
    Team side;
    List<GameObject> enemyBuildings;
    List<GameObject> enemyUnits;
    float fovDist = 20.0f;
    float fovAngle = 45.0f;

    public SearchState(Unit _unit) : base(_unit.gameObject)
    {
        unit = _unit;
        navMeshAgent = unit.GetComponent<NavMeshAgent>();
        enemyUnits = GameObject.FindGameObjectsWithTag("Unit").ToList();
        side = unit.GetComponent<Team>();
    }

    public override Type Tick()
    {
        //unit.CurrentTarget = CanSee() ? unit.CurrentTarget = FindNearestEnemy().gameObject : FindNearestBuilding();
        if (CanSee())
        {
            unit.CurrentTarget = CanSee();
        }
        if (unit.CurrentTarget == null)
        {
            unit.CurrentTarget = FindNearestBuilding();
        }

        //Debug.Log(unit.CurrentTarget.gameObject.name);
        this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, Quaternion.LookRotation(unit.CurrentTarget.transform.position - unit.transform.position), Time.deltaTime * 5);
        navMeshAgent.isStopped = false;

        if (unit.CurrentTarget == null)
        {
            unit.CurrentTarget = FindNearestBuilding();
            return typeof(SearchState);
        }
        else if (unit.CurrentTarget != null)
        {
            if (Vector3.Distance(unit.transform.position, unit.CurrentTarget.transform.position) < 3.5f)
            {
                navMeshAgent.isStopped = true;
                //Debug.Log("Attack");
                return typeof(AttackState);
                
            }
            navMeshAgent.SetDestination(unit.CurrentTarget.transform.position);
            return typeof(SearchState);
        }
        unit.CurrentTarget = null;
        return typeof(SearchState);
    }

    GameObject CanSee()
    {
        Vector3 direction = transform.forward;
        float angle = Vector3.Angle(direction, this.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(this.gameObject.transform.position, transform.forward, out hit) && hit.collider.gameObject.GetComponent<Team>().team != unit.gameObject.GetComponent<Team>().team
            && direction.magnitude < fovDist)
        {
            if (hit.collider.gameObject.GetComponent<Unit>() != null)
            {
                //Debug.DrawRay(transform.position, hit.collider.transform.position, Color.red, 10f);
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    private GameObject FindNearestBuilding()
    {
        List<GameObject> enemyBuildings = side.teamBase.enemyBuildings.ToList();
        GameObject closestBuilding = enemyBuildings.First();
        foreach (GameObject gameObject in enemyBuildings)
        {
            if (gameObject.GetComponent<Building>().state == Building.State.Intact)
            {
                closestBuilding = gameObject;
            }
        }
        foreach (var building in enemyBuildings)
        {
            if (building != null && building.GetComponent<Building>().state == Building.State.Intact)
            {
                if (Vector3.Distance(unit.transform.position, building.transform.position) < Vector3.Distance(unit.transform.position, closestBuilding.transform.position))
                {
                    closestBuilding = building;
                }
            }
        }
        
        return closestBuilding;
    }

    private Transform FindNearestEnemy()
    {
        Transform closestEnemy = this.transform;
        //closestEnemy.position = Vector3.zero;
        if (enemyUnits != null)
        {

            closestEnemy = unit.gameObject.GetComponent<Team>().team == Team.side.Blue ? enemyUnits.Last().transform : enemyUnits.First().transform;
            foreach (var _unit in enemyUnits)
            {
                if (_unit != null)
                {
                    if (Vector3.Distance(unit.transform.position, _unit.transform.position) < Vector3.Distance(unit.transform.position, closestEnemy.transform.position))
                    {
                        closestEnemy = _unit.transform;
                    }
                }
            }
            return closestEnemy;
        }
        Debug.Log("No Enemies");
        return closestEnemy;
    }
}