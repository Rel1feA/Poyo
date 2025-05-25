using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStateType
{
    Idle,
    Operator,
    Dead
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
        player.controller.SetVel(0);
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
    private bool canJump;

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
        player.controller.CheckSwitch();
        player.controller.ChangeGravityScale();


        if (player.controller.isOnWake) return;
        //上楼梯，并且下面的移动等操作不执行
        if (player.controller.CanClimb && !player.controller.IsClimb)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                player.controller.EnterClimb();
            }
        }
        if (player.controller.IsClimb)
        {
            player.controller.OnClimb();
            return;
        }
        //

        //使用技能
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.controller.UseSkill();
            player.controller.SetVel(0);
        }
        //

        //设置动画
        player.controller.SetNormalAni();
        //

        //移动
         player.controller.Move();
        //

        if (Input.GetKeyDown(KeyCode.Space) && !canJump && player.controller.CheckGround())
        {
            canJump = true;
        }

    }

    public override void PhysicsUpdate()
    {
        //跳跃
        if (canJump)
        {
            player.controller.Jump();
            canJump = false;
        }
        //
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
#endregion

#region 玩家死亡状态类
public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(StateMachine _stateMachine, Player player) : base(_stateMachine, player)
    {

    }

    public override void EnterState()
    {
        player.Dead();
    }

    public override void ExitState()
    {

    }

    public override void FrameUpdate()
    {

    }
} 
#endregion