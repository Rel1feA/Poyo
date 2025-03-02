using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttack : BomberManState
{
    public BombAttack(StateMachine _stateMachine, BomberMan bomberMan) : base(_stateMachine, bomberMan)
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
        if(!bomberMan.CheckAttackPlayer())
        {
            stateMachine.Changestate(bomberMan.patrolState);
        }
    }
}
