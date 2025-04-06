using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IOperator,ITakeDamge
{
    public bool canOperator { get; set; }
    public bool hasDead { get; set; }

    public virtual void EnterOperator()
    {
        
    }

    public virtual void ExitOperator()
    {
        
    }

    public virtual GameObject GetOperatorObject()
    {
        return null;
    }

    public virtual void OnOperator()
    {
        
    }

    public virtual void TakeDamge(float damage, UnityAction callback = null)
    {
        
    }
}
