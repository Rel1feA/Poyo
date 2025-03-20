using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleState : State
{
    public Turtle turtle;

    public TurtleState(StateMachine _stateMachine,Turtle turtle) : base(_stateMachine)
    {
        this.turtle = turtle;
    }
}

public class TurtleIdle : TurtleState
{
    public TurtleIdle(StateMachine _stateMachine, Turtle turtle) : base(_stateMachine, turtle)
    {

    }

    public override void EnterState()
    {
        turtle.rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

    }

    public override void ExitState()
    {
        turtle.rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public override void FrameUpdate()
    {
        turtle.SetVelocity(0);
    }

    public override void PhysicsUpdate()
    {

    }
}

public class TurtleContro : TurtleState
{
    public TurtleContro(StateMachine _stateMachine, Turtle turtle) : base(_stateMachine, turtle)
    {

    }

    public override void EnterState()
    {
        
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        turtle.Move();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OperatorManager.Instance.ChangeOperator(turtle.player.controller);
            stateMachine.Changestate(turtle.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }
}

