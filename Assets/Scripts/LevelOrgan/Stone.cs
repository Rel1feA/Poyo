using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stone : MonoBehaviour, ITakeDamge
{
    public float maxHealth;
    private float health;

    private void Start()
    {
        health=maxHealth;
    }

    public void TakeDamge(float damage, UnityAction callback = null)
    {
        health -= damage;
        if(health<=0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
