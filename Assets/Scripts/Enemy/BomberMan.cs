using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BomberMan : MonoBehaviour, IOperator,ITakeDamge
{
    public StateMachine stateMachine;
    public BomberManState patrolState;
    public BomberManState IdleState;
    public BomberManState controlState;
    public BomberManState hurtState;
    public BomberManState attackState;

    [HideInInspector]
    public Rigidbody2D rb2D;
    [HideInInspector]
    public Player player;

    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed;

    public float moveSpeed;

    public float attackRadius;


    private void Start()
    {
        player=FindObjectOfType<Player>();
        InitState();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        stateMachine.currentState.FrameUpdate();
    }

    public void EnterOperator()
    {
        stateMachine.Changestate(controlState);
    }

    public void OnOperator()
    {

    }

    public void ExitOperator()
    {
        
    }

    public void InitState()
    {
        stateMachine = new StateMachine();
        patrolState = new BombPatrol(stateMachine,this);
        controlState=new BombControl(stateMachine,this);
        hurtState=new BombHurt(stateMachine,this);
        attackState=new BombAttack(stateMachine,this);
        stateMachine.Init(patrolState);
    }

    public void TakeDamge(UnityAction callback)
    {
        stateMachine.Changestate(hurtState);
        callback?.Invoke();
    }

    public bool CheckAttackPlayer()
    {
        if(Vector3.Distance(player.transform.position,transform.position)<attackRadius)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,attackRadius);
    }
}
