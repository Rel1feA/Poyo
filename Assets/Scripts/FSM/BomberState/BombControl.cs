using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        CheckSwitch();
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //if (bomberMan.player == null) return;
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

    public void CheckSwitch()
    {
        Collider2D collider2D = Physics2D.OverlapBox(bomberMan.transform.position, Vector2.one * 1.5f, 0, LayerMask.GetMask("Switch"));
        if (collider2D == null) return;
        if (collider2D.TryGetComponent<ISwitch>(out ISwitch _switch))
        {
            _switch.Trigger();
        }
    }
}
