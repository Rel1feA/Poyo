using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPatrol : BomberManState
{
    private float patrolDir;
    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 targetPos;

    public BombPatrol(StateMachine _stateMachine, BomberMan bomberMan) : base(_stateMachine, bomberMan)
    {

    }

    public override void EnterState()
    {
        patrolDir = bomberMan.transform.right.x;
        startPos=bomberMan.transform.position;
        endPos=startPos+(Vector2)bomberMan.transform.right*bomberMan.patrolDistance;
        targetPos = endPos;
    }

    public override void ExitState()
    {
        
    }

    public override void FrameUpdate()
    {
        Patrol();
    }

    public void Patrol()
    {
        bomberMan.rb2D.velocity=new Vector2(bomberMan.patrolSpeed*patrolDir,bomberMan.rb2D.velocity.y);
        if (Vector2.Distance(bomberMan.transform.position, targetPos) < 0.1f)
        {
            patrolDir = -patrolDir;
            if(targetPos==startPos)
            {
                targetPos = endPos;
            }
            else if(targetPos==endPos)
            {
                targetPos= startPos;
            }
        }
    }
}
