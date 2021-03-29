using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private float attackSpeed;
    private int attackDamage;
    private float movementSpeed;
    private float hp = 30;

    public GameObject CurrentTarget;
    [SerializeField]
    public Damageable damageable;
    public Team team;
    public float HP { get => hp; private set => hp = value; }
    public float MoveSpeed { get => movementSpeed; }
    public int AttackDamage { get => attackDamage; }


    private void Start()
    {
        team = GetComponent<Team>();
        damageable = gameObject.GetComponent<Damageable>();
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
        foreach (GameObject Base in bases)
        {
            if (Base.GetComponent<Team>().team == team.team)
            {
                team.teamBase = Base.GetComponent<Base>();
            }
        }
    }

    public virtual void Attack(int damageAmount)
    {

    }

    private void Awake()
    {
        damageable.OnDamaged += TakeDamage;
        damageable.OnDoDamage += Attack;
    }

    /*private void OnEnable()
    {
        damageable.OnDamaged += TakeDamage;
        damageable.OnDoDamage += Attack;
    }*/

    private void OnDisable()
    {
        damageable.OnDamaged -= TakeDamage;
        damageable.OnDoDamage -= Attack;
    }

    public void TakeDamage(int damageAmount)
    {
        //Debug.Log("Hit Unit");

        HP -= damageAmount;
        if (HP <= 0)
        {
            Destroy(this.gameObject);
        }
        //Debug.Log($"{gameObject.GetComponent<Team>().team} : {HP}");
    }
}