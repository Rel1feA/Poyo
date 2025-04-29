using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BombControl : BomberManState
{
    private float timer;
    private float inputX;

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
        SetAni();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //if (bomberMan.player == null) return;
            OperatorManager.Instance.ChangeOperator(bomberMan.player.controller);
            stateMachine.Changestate(bomberMan.dizzyState);
        }
        if(Input.GetKeyDown(KeyCode.E)&&timer>=bomberMan.useCoolTime)
        {
            timer = 0;
            bomberMan.ThrowBomb(bomberMan.throwSpeed*new Vector2(bomberMan.transform.localScale.x,0.7f));
        }
    }

    public void Move()
    {
        inputX = Input.GetAxis("Horizontal");
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

    public void SetAni()
    {
        if(inputX!=0)
        {
            bomberMan.AniSetTrigger("Walk");
        }
        else
        {
            bomberMan.AniSetTrigger("Idle");
        }
    }
}
