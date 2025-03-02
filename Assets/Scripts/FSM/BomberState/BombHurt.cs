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
        Debug.Log("hurt");
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        
    }
}
