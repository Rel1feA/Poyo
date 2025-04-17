using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateType
{
    Idle,
    Operator
}

public class PlayerState : State
{
    public Player player;

    public PlayerState(StateMachine _stateMachine,Player player) : base(_stateMachine)
    {
        this.player = player;
    }
}

#region PlayerIdle状态类
public class PlayerIdle : PlayerState
{
    public PlayerIdle(StateMachine _stateMachine, Player player) : base(_stateMachine, player)
    {

    }

    public override void EnterState()
    {
        player.controller.AniSetTrigger("Sleep");
        //player.controller.rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public override void ExitState()
    {
        //player.controller.rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public override void FrameUpdate()
    {
        player.controller.CheckSwitch();
        player.controller.ChangeGravityScale();
    }

    public override void PhysicsUpdate()
    {
        
    }
}
#endregion

#region Player被操控状态类
public class PlayerOperator : PlayerState
{
    public PlayerOperator(StateMachine _stateMachine, Player player) : base(_stateMachine, player)
    {

    }

    public override void EnterState()
    {
        player.controller.isOnWake=true;
        player.controller.AniSetTrigger("Wake");
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        player.controller.BeOnOperator();
        player.controller.CheckSwitch();
        player.controller.ChangeGravityScale();
    }

    public override string ToString()
    {
        return base.ToString();
    }
} 
#endregion