using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DizzyState : BomberManState
{
    private float timer;

    public DizzyState(StateMachine _stateMachine, BomberMan bomberMan) : base(_stateMachine, bomberMan)
    {

    }

    public override void EnterState()
    {
        timer = 0;
        bomberMan.rb2D.velocity = new Vector2(0, bomberMan.rb2D.velocity.y);
        bomberMan.spriteRenderer.color = Color.green;
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        timer += Time.deltaTime;
        if(timer>=bomberMan.dizzyTime)
        {
            if(bomberMan.CheckAttackPlayer())
            {
                stateMachine.Changestate(bomberMan.attackState);
            }
            else
            {
                stateMachine.Changestate(bomberMan.patrolState);
            }
        }
    }
}
