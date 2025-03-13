using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        patrolDir =bomberMan.transform.localScale.x;
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        //Patrol();
        WallPatrol();
        if(bomberMan.CheckWallPatrol())
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
        if(Vector3.Distance(bomberMan.transform.position,targetPos)<0.5f)
        {
            if(targetPos==bomberMan.pointA.position)
            {
                targetPos=bomberMan.pointB.position;
            }
            else if(targetPos == bomberMan.pointB.position)
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
        bomberMan.rb2D.velocity=new Vector2(patrolDir*bomberMan.patrolSpeed,bomberMan.rb2D.velocity.y);
    }
}
