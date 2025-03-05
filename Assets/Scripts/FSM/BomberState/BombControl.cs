using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : BomberManState
{
    private float timer;

    public BombControl(StateMachine _stateMachine, BomberMan bomberMan) : base(_stateMachine, bomberMan)
    {

    }

    public override void EnterState()
    {
        timer = bomberMan.useCoolTime;
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        timer += Time.deltaTime;
        Move();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            OperatorManager.Instance.ChangeOperator(bomberMan.player);
            stateMachine.Changestate(bomberMan.patrolState);
        }
        if(Input.GetKeyDown(KeyCode.E)&&timer>=bomberMan.useCoolTime)
        {
            timer = 0;
            bomberMan.ThrowBomb(bomberMan.throwSpeed*new Vector2(bomberMan.transform.localScale.x,0.7f));
        }
    }

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        bomberMan.rb2D.velocity = new Vector2(bomberMan.moveSpeed*inputX,bomberMan.rb2D.velocity.y);
        if(inputX > 0)
        {
            bomberMan.transform.localScale=new Vector3(1,1,1);
        }
        else if(inputX < 0)
        {
            bomberMan.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
