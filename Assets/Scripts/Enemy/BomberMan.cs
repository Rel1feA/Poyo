using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMan : MonoBehaviour, IOperator
{
    public StateMachine stateMachine;
    public BomberManState patrolState;
    public BomberManState IdleState;
    public BomberManState controlState;
    public Rigidbody2D rb2D;

    public float patrolDistance;
    public float patrolSpeed;

    public float moveSpeed;


    private void Start()
    {
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
        stateMachine.Init(patrolState);
    }
}
