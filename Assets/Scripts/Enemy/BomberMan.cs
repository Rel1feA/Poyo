using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BomberMan : MonoBehaviour, IOperator,ITakeDamge
{
    public StateMachine stateMachine;
    public BomberManState patrolState;
    public BomberManState IdleState;
    public BomberManState controlState;
    public BomberManState hurtState;
    public BomberManState attackState;

    [HideInInspector]
    public Rigidbody2D rb2D;
    [HideInInspector]
    public Player player;

    [Header("巡逻相关")]
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed;

    [Header("控制相关")]
    public float moveSpeed;
    public float useCoolTime;

    [Header("攻击相关")]
    public float attackRadius;

    [Header("技能相关")]
    public float throwForce;
    public float attackCoolTime;


    private void Start()
    {
        player=FindObjectOfType<Player>();
        InitState();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        stateMachine.currentState.FrameUpdate();
    }

    public void EnterOperator()
    {
        stateMachine.Changestate(controlState);
    }

    public void OnOperator()
    {

    }

    public void ExitOperator()
    {
        
    }

    public void InitState()
    {
        stateMachine = new StateMachine();
        patrolState = new BombPatrol(stateMachine,this);
        controlState=new BombControl(stateMachine,this);
        hurtState=new BombHurt(stateMachine,this);
        attackState=new BombAttack(stateMachine,this);
        stateMachine.Init(patrolState);
    }

    public void TakeDamge(UnityAction callback)
    {
        stateMachine.Changestate(hurtState);
        callback?.Invoke();
    }

    public bool CheckAttackPlayer()
    {
        if(Vector3.Distance(player.transform.position,transform.position)<attackRadius)
        {
            return true;
        }
        return false;
    }

    public void ThrowBomb()
    {
        ResourcesManager.Instance.LoadAsync<GameObject>("Prefabs/Bomb", (obj) =>
        {
            Vector3 throwDir = new Vector3(transform.localScale.x, 1, 0);
            obj.transform.position = transform.position + throwDir;
            obj.GetComponent<Rigidbody2D>().AddForce(throwDir * throwForce, ForceMode2D.Impulse);
        });
    }

    public void LookAtPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,attackRadius);
    }
}
