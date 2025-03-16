using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombIdleState : BomberManState
{
    public BombIdleState(StateMachine _stateMachine, BomberMan bomberMan) : base(_stateMachine, bomberMan)
    {
    }

    private float timer;

    public override void EnterState()
    {
        timer = 0;
        bomberMan.rb2D.velocity=new Vector2(0,bomberMan.rb2D.velocity.y);
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        bomberMan.rb2D.velocity = new Vector2(0, bomberMan.rb2D.velocity.y);
        bomberMan.spriteRenderer.color = Color.yellow;
        timer += Time.deltaTime;
        if (bomberMan.CheckAttackPlayer())
        {
            stateMachine.Changestate(bomberMan.attackState);
        }
        if (timer > bomberMan.waitTime)
        {
            bomberMan.Filp();
            stateMachine.Changestate(bomberMan.patrolState);
        }
    }
}
