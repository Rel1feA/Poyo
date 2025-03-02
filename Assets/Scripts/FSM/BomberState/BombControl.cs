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
        if(Input.GetKeyDown(KeyCode.Q))
        {
            OperatorManager.Instance.ChangeOperator(bomberMan.player);
            stateMachine.Changestate(bomberMan.patrolState);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            UseSkill();
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

    public void UseSkill()
    {
        ResourcesManager.Instance.LoadAsync<GameObject>("Prefabs/Bomb",(obj)=>
        {
            Vector3 throwDir = new Vector3(bomberMan.transform.localScale.x, 1,0);
            obj.transform.position = bomberMan.transform.position + throwDir;
            obj.GetComponent<Rigidbody2D>().AddForce(throwDir*6,ForceMode2D.Impulse);
        });
    }
}
