using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttack : BomberManState
{
    private float timer;

    public BombAttack(StateMachine _stateMachine, BomberMan bomberMan) : base(_stateMachine, bomberMan)
    {

    }

    public override void EnterState()
    {
        bomberMan.LookAtPlayer();
        timer = bomberMan.attackCoolTime-0.5f;
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        timer += Time.deltaTime;
        if(timer > bomberMan.attackCoolTime)
        {
            bomberMan.LookAtPlayer();
            bomberMan.ThrowBomb(bomberMan.GetThrowBombToPlayerSpeed(0.7f));
            timer= 0;
        }
        if(!bomberMan.CheckAttackPlayer()&&timer>1)
        {
            stateMachine.Changestate(bomberMan.patrolState);
        }
    }
}
