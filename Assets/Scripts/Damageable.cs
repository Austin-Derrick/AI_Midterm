using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public delegate void Damaged(int damageAmount);
    public event Damaged OnDamaged;

    public delegate void Damage(int damageAmount);
    public event Damage OnDoDamage;
    Team side;

    private void Start()
    {
        side = GetComponent<Team>();
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.tag == "Unit" || other.gameObject.tag == "Building")
        { 
            if (side.team == other.gameObject.GetComponent<Team>().team)
            {
                return;
            }
            else
            {
                OnDamaged?.Invoke(10);
            }
        }*/
    }

    public void hitByUnit()
    {
        OnDamaged?.Invoke(10);
    }

}