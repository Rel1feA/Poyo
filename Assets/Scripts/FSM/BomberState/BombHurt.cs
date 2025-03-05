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
        OperatorManager.Instance.ChangeOperator(bomberMan.player);
        GameObject.Destroy(bomberMan.gameObject);
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        
    }
}
