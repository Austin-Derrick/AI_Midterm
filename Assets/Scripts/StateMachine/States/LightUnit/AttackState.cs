using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public GameObject Target;
    public Unit unitInfo;

    float distanceToTarget;
    float count = 0.5f;
    float waitTime = 1f;

    public AttackState(Unit unit) : base(unit.gameObject)
    {
        unitInfo = unit;
    }

    public override Type Tick()
    {
        if (unitInfo.CurrentTarget == null)
        {
            return typeof(SearchState);
        }
        distanceToTarget = Vector3.Distance(gameObject.transform.position, unitInfo.CurrentTarget.transform.position);
        //Debug.Log(Mathf.Abs(distanceToTarget));
        

        if (Mathf.Abs(distanceToTarget) > 3.6f)
        {/*
            Debug.Log("Chase");*/
            count = 0.5f;
            return typeof(SearchState);

        }
        else
        {
            //nitInfo.transform.LookAt(unitInfo.CurrentTarget.transform);
            Debug.DrawRay(unitInfo.transform.position + unitInfo.transform.right, unitInfo.CurrentTarget.transform.position - unitInfo.transform.position, Color.red);
            if (count >= waitTime)
            {
                RaycastHit hit;
                if (Physics.Raycast(unitInfo.transform.position + unitInfo.transform.right, unitInfo.CurrentTarget.transform.position - unitInfo.transform.position, out hit, 10f))
                {
                    if (hit.collider.gameObject.GetComponent<Damageable>() != null)
                    {
                        //Debug.Log(hit.collider.gameObject.name);
                        if (hit.collider.gameObject.GetComponent<Building>() !=  null && hit.collider.gameObject.GetComponent<Building>().state != Building.State.Destroyed)
                        {
                            hit.collider.gameObject.GetComponent<Damageable>().hitByUnit();
                            Debug.Log("Hit building");
                            return (typeof(AttackState));
                        }
                        else if (hit.collider.GetComponent<Unit>() != null)
                        {
                            hit.collider.gameObject.GetComponent<Damageable>().hitByUnit();
                            return (typeof(AttackState));
                        }
                        return (typeof(SearchState));
                        //Debug.Log(unitInfo.CurrentTarget.name);
                    }
                    else
                    {
                        //Debug.Log("Search");
                        return typeof(SearchState);
                    }
                }
                //unitInfo.Attack(10);
                count = 0;
            }
            else
            {
                count += Time.deltaTime;
            }

            //}
            /*Debug.Log("Attack");*/
            return typeof(AttackState);
        }
        return typeof(AttackState);
    }
}