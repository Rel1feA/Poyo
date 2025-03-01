using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMan : MonoBehaviour, IOperator
{
    public StateMachine stateMachine;
    public BomberManState patrolState;
    public Rigidbody2D rb2D;

    public float patrolDistance;
    public float patrolSpeed;

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
        stateMachine.Init(patrolState);
    }
}
