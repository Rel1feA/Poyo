using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum BomberManStateType
{
    Idle,
    Control,
    Hurt,
    Patrol,
    Dizzy
}

public class BomberManState : State
{
    public BomberMan bomberMan;
    public BomberManState(StateMachine _stateMachine,BomberMan bomberMan) : base(_stateMachine)
    {
        this.bomberMan = bomberMan;
    }
}

#region 炸弹人被控制类
public class BombControl : BomberManState
{
    private float timer;
    private float inputX;
    private bool isThrowBomb;

    public BombControl(StateMachine _stateMachine, BomberMan bomberMan) : base(_stateMachine, bomberMan)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Control");
        timer = bomberMan.useCoolTime;
    }

    public override void ExitState()
    {
        bomberMan.parabolicPred.gameObject.SetActive(false);
    }

    public override void FrameUpdate()
    {
        timer += Time.deltaTime;
        Move();
        SetAni();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //if (bomberMan.player == null) return;
            OperatorManager.Instance.ChangeOperator(bomberMan.player.controller);
            bomberMan.ChangeState(BomberManStateType.Dizzy);
            bomberMan.AniSetTrigger("Sleep");
        }
        if(timer >= bomberMan.useCoolTime)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                bomberMan.parabolicPred.gameObject.SetActive(true);
                bomberMan.targetSpeed = bomberMan.minThrowSpeed;
                bomberMan.detalAddSpeed = 2.5f;
                isThrowBomb = true;
            }
            if (Input.GetKey(KeyCode.E)&&isThrowBomb)
            {
                bomberMan.CaluateThrowForce();
                List<Vector2> points = bomberMan.parabolicPred.CalculateTrajectory
                    ((Vector2)bomberMan.transform.position +new Vector2(bomberMan.transform.localScale.x *bomberMan.throwOffset.x,bomberMan.throwOffset.y),
                    new Vector2(bomberMan.transform.localScale.x, 1).normalized *bomberMan.targetSpeed);
                bomberMan.parabolicPred.DrawTrajectory(points);
            }
            if (Input.GetKeyUp(KeyCode.E)&&isThrowBomb)
            {
                timer = 0;
                isThrowBomb=false;
                bomberMan.ThrowBomb();
                bomberMan.parabolicPred.gameObject.SetActive(false);
            }
        }
    }

    public void Move()
    {
        inputX = Input.GetAxis("Horizontal");
        bomberMan.rb2D.velocity = new Vector2(bomberMan.moveSpeed * inputX, bomberMan.rb2D.velocity.y);
        if (inputX > 0)
        {
            bomberMan.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputX < 0)
        {
            bomberMan.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void SetAni()
    {
        if (inputX != 0)
        {
            bomberMan.AniSetTrigger("Walk");
        }
        else
        {
            bomberMan.AniSetTrigger("Idle");
        }
    }
}
#endregion

#region 炸弹人巡逻类
public class BombPatrol : BomberManState
{
    private float patrolDir;
    private Vector3 targetPos;

    public BombPatrol(StateMachine _stateMachine, BomberMan bomberMan) : base(_stateMachine, bomberMan)
    {

    }

    public override void EnterState()
    {
        //if(Vector2.Distance(bomberMan.transform.position,bomberMan.pointA.position)> Vector2.Distance(bomberMan.transform.position, bomberMan.pointB.position))
        //{
        //    targetPos = bomberMan.pointA.position;
        //}
        //else
        //{
        //    targetPos = bomberMan.pointB.position;
        //}
        //if (targetPos.x > bomberMan.transform.position.x)
        //{
        //    bomberMan.transform.localScale = Vector3.one;
        //}
        //else if (targetPos.x < bomberMan.transform.position.x)
        //{
        //    bomberMan.transform.localScale = new Vector3(-1, 1, 1);
        //}
        Debug.Log("Patrol");
        bomberMan.AniSetTrigger("Walk");
        patrolDir = bomberMan.transform.localScale.x;
    }

    public override void ExitState()
    {

    }

    public override void FrameUpdate()
    {
        //Patrol();
        WallPatrol();
        if (bomberMan.CheckWallPatrol())
        {
            stateMachine.Changestate(bomberMan.idleState);
        }
        if (bomberMan.CheckAttackPlayer())
        {
            stateMachine.Changestate(bomberMan.attackState);
        }
    }

    public void Patrol()
    {
        if (Vector3.Distance(bomberMan.transform.position, targetPos) < 0.5f)
        {
            if (targetPos == bomberMan.pointA.position)
            {
                targetPos = bomberMan.pointB.position;
            }
            else if (targetPos == bomberMan.pointB.position)
            {
                targetPos = bomberMan.pointA.position;
            }
            bomberMan.transform.localScale = new Vector3(-bomberMan.transform.localScale.x, 1, 1);
        }
        Vector3 newPos = Vector3.MoveTowards(bomberMan.transform.position, targetPos, bomberMan.patrolSpeed * Time.fixedDeltaTime);
        bomberMan.rb2D.MovePosition(newPos);
    }

    public void WallPatrol()
    {
        bomberMan.rb2D.velocity = new Vector2(patrolDir * bomberMan.patrolSpeed, bomberMan.rb2D.velocity.y);
    }
}
#endregion

#region 炸弹人Idle状态类
public class BombIdleState : BomberManState
{
    private float timer;

    public BombIdleState(StateMachine _stateMachine, BomberMan bomberMan) : base(_stateMachine, bomberMan)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Idle");
        bomberMan.AniSetTrigger("Idle");
        timer = 0;
        bomberMan.rb2D.velocity = new Vector2(0, bomberMan.rb2D.velocity.y);
    }

    public override void FrameUpdate()
    {
        bomberMan.rb2D.velocity = new Vector2(0, bomberMan.rb2D.velocity.y);
        timer += Time.deltaTime;
        if(timer>bomberMan.waitTime)
        {
            bomberMan.ChangeState(BomberManStateType.Patrol);
            bomberMan.Filp();
        }
    }
}
#endregion

#region 炸弹人沉睡类
public class DizzyState : BomberManState
{
    public DizzyState(StateMachine _stateMachine, BomberMan bomberMan) : base(_stateMachine, bomberMan)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Dizzy");
        bomberMan.rb2D.velocity = new Vector2(0, bomberMan.rb2D.velocity.y);
    }
} 
#endregion
