using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(StateMachine))]
[RequireComponent(typeof(NavMeshAgent))]

public class LightUnit : Unit
{
    StateMachine stateMachine => GetComponent<StateMachine>();
    BoxCollider collider => GetComponent<BoxCollider>();
    

    // Start is called before the first frame update
    void OnEnable()
    {
        damageable = GetComponent<Damageable>();
        IntializeStateMachine();
    }

    void IntializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(SearchState), new SearchState(this)},
            { typeof(AttackState), new AttackState(this)}/*,
            { typeof(TauntState), new TauntState(this)},
            { typeof(DefendState), new DefendState(this)},
            { typeof(FleeState), new FleeState(this)}*/
        };
        stateMachine.SetStates(states);
    }

    public override void Attack(int damage)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}