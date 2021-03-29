using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Team))]

public class Building : MonoBehaviour
{
    private float health;
    public enum State { Intact, Destroyed }
    public State state;
    public float Health { get => health; private set => health = value; }
    public Unit unitToSpawn;
    [SerializeField]
    public Damageable damageable;
    [SerializeField]
    public Destructable destructable;
    Team team;
    float count = 9f;
    float waitTime = 10f;
    GameObject unit;
    // Start is called before the first frame update
    void Start()
    {
        damageable = GetComponent<Damageable>();
        destructable = GetComponent<Destructable>();
        state = State.Intact;
        Health = 30;
        InvokeRepeating("spawnUnit", 1f, 10f);
        /*Invoke("spawnUnit", 1f);*/
    }

    public void spawnUnit()
    {
        if (unitToSpawn != null)
        {
            unit = Instantiate(unitToSpawn.gameObject, this.transform.position - this.transform.forward, unitToSpawn.transform.rotation);   

        }
    }

    // Update is called once per frame
    void Update()
    {   
        /*if (count >= waitTime)
        {
            Debug.Log("Spawn Unit");
            Debug.Log("Spawn Unit");
            unit = Instantiate(unitToSpawn.gameObject, this.transform.position, unitToSpawn.transform.rotation);
            count = 0;
        }
        else
        {
            count += Time.deltaTime;
        }*/
    }

    private void OnEnable()
    {
        damageable.OnDamaged += TakeDamage;
    }

    private void OnDisable()
    {
        damageable.OnDamaged -= TakeDamage;
    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
        if (health <= 0)
        {
            state = State.Destroyed;
            //Destroy(this.gameObject);
        }
    }
}