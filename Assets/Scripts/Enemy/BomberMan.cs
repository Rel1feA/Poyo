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

    [Header("基本属性")]
    public float maxHealth;
    private float currentHealth;

    [Header("巡逻相关")]
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed;

    [Header("控制相关")]
    public float moveSpeed;


    [Header("攻击相关")]
    public float attackRadius;
    public float bombToPlayerTime;//投掷炸弹到玩家身旁的炸弹飞行期望时间
    public float attackCoolTime;

    [Header("技能相关")]
    public float throwSpeed;
    public float useCoolTime;

    private void OnEnable()
    {
        EventCenter.Instance.AddListener("PlayerDead",OnPlayerDead);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("PlayerDead",OnPlayerDead);
    }


    private void Start()
    {
        player=FindObjectOfType<Player>();
        InitState();
        rb2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
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
    public GameObject GetOperatorObject()
    {
        return gameObject;
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

    public void TakeDamge(float damage,UnityAction callback)
    {
        currentHealth-=damage;
        if(currentHealth <= 0)
        {
            stateMachine.Changestate(hurtState);
        }
        callback?.Invoke();
    }

    public bool CheckAttackPlayer()
    {
        if (player == null) return false;
        if(Vector3.Distance(player.transform.position,transform.position)<attackRadius)
        {
            return true;
        }
        return false;
    }

    public void ThrowBomb(Vector2 speed)
    {
        ResourcesManager.Instance.LoadAsync<GameObject>("Prefabs/Bomb", (obj) =>
        {
            obj.transform.position = (Vector2)transform.position + new Vector2(transform.localScale.x, 0.7f);
            obj.GetComponent<Rigidbody2D>().velocity= speed;
        });
    }

    public Vector2 GetThrowBombToPlayerSpeed(float time)
    {
        Vector2 throwPos = (Vector2)transform.position + new Vector2(transform.localScale.x, 0.7f);
        float horizontalSpeed = Mathf.Abs(player.transform.position.x - throwPos.x) / time * transform.localScale.x;
        //float verticalSpeed = Mathf.Abs(player.transform.position.y - throwPos.y) / time - (0.5f  *2* Physics2D.gravity.y * time);
        float verticalSpeed = (-2 * Physics2D.gravity.y * time / 2)*0.7f;
        Vector2 throwSpeed = new Vector2(horizontalSpeed, verticalSpeed);
        return throwSpeed;
    }

    public void LookAtPlayer()
    {
        if (player == null) return;
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void OnPlayerDead()
    {
        player = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,attackRadius);
    }
}
