using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHurt : BomberManState
{
    public BombHurt(StateMachine _stateMachine, BomberMan bomberMan) : base(_stateMachine, bomberMan)
    {

    }

    public override void EnterState()
    {
        if(OperatorManager.Instance.GetCurrentOperatorObj()!=null)
        {
            if(OperatorManager.Instance.GetCurrentOperatorObj()==bomberMan.gameObject)
            {
                OperatorManager.Instance.ChangeOperator(bomberMan.player.controller);
            }
        }
        bomberMan.AniSetTrigger("Dead");
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        
    }
}
