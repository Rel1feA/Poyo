using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CrabStateType
{
    Idle,
    Control,
    Dead
}


public class CrabState : State
{
    public Crab crab;

    public CrabState(StateMachine _stateMachine,Crab turtle) : base(_stateMachine)
    {
        this.crab = turtle;
    }
}

#region Crab的Idle状态类
public class CrabIdle : CrabState
{
    public CrabIdle(StateMachine _stateMachine, Crab turtle) : base(_stateMachine, turtle)
    {

    }

    public override void EnterState()
    {
        crab.animator.SetTrigger("ExitControl");
        crab.rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public override void ExitState()
    {
        crab.rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public override void FrameUpdate()
    {
        crab.SetVelocity(0);
    }

    public override void PhysicsUpdate()
    {

    }
}
#endregion

#region Crab的被操控状态类
public class CrabContro : CrabState
{
    public CrabContro(StateMachine _stateMachine, Crab turtle) : base(_stateMachine, turtle)
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
        crab.Move();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OperatorManager.Instance.ChangeOperator(crab.player.controller);
            crab.ChangeState(CrabStateType.Idle);
        }
    }

    public override void PhysicsUpdate()
    {

    }
} 
#endregion

