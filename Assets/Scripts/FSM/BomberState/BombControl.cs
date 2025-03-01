using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : BomberManState
{
    public BombControl(StateMachine _stateMachine, BomberMan bomberMan) : base(_stateMachine, bomberMan)
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
        Move();
    }

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        bomberMan.rb2D.velocity = new Vector2(bomberMan.moveSpeed*inputX,bomberMan.rb2D.velocity.y);
    }
}
