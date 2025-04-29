using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BomberMan : Enemy,ITakeDamge,IOperator
{
    public StateMachine stateMachine;
    public BomberManState idleState;
    public BomberManState patrolState;
    public BomberManState dizzyState;
    public BomberManState controlState;
    public BomberManState hurtState;
    public BomberManState attackState;

    [HideInInspector]
    public Rigidbody2D rb2D;
    [HideInInspector]
    public Player player;

    [Header("基本属性")]
    public float maxHealth;
    public float dizzyTime;
    private float currentHealth;

    [Header("巡逻相关")]
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed;
    public float waitTime;

    [Header("控制相关")]
    public float moveSpeed;


    [Header("攻击相关")]
    public bool canAttack;
    public Vector2 attackCheckBox;
    public float bombToPlayerTime;//投掷炸弹到玩家身旁的炸弹飞行期望时间
    public float attackCoolTime;

    [Header("技能相关")]
    public float throwSpeed;
    public float useCoolTime;

    [Header("动画相关")]
    private string previousTrigger;
    [HideInInspector]
    public Animator animator;

    private void Start()
    {
        player=FindObjectOfType<Player>();
        rb2D = GetComponent<Rigidbody2D>();
        animator= GetComponent<Animator>();
        InitState();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        stateMachine.currentState.FrameUpdate();
        CheckSwitch();
    }

    private void LateUpdate()
    {
        stateMachine.CheckChangeState();
    }

    #region 实现进入操控模式的方法
    public override void EnterOperator()
    {
        stateMachine.Changestate(controlState);
        canOperator = true;
    }
    #endregion

    #region 实现操控接口返回obj的方法
    public override GameObject GetOperatorObject()
    {
        return gameObject;
    } 
    #endregion

    #region 初始化状态
    public void InitState()
    {
        stateMachine = new StateMachine();
        idleState = new BombIdleState(stateMachine, this);
        dizzyState = new DizzyState(stateMachine, this);
        patrolState = new BombPatrol(stateMachine, this);
        controlState = new BombControl(stateMachine, this);
        hurtState = new BombHurt(stateMachine, this);
        attackState = new BombAttack(stateMachine, this);
        stateMachine.Init(patrolState);
    }
    #endregion

    #region 实现ITakeDamge接口方法
    public override void TakeDamge(float damage, UnityAction callback)
    {
        if (hasDead) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            stateMachine.Changestate(hurtState);
            hasDead = true;
        }
        callback?.Invoke();
    }
    #endregion

    #region 检测玩家是否在攻击范围内的方法
    public bool CheckAttackPlayer()
    {
        if (!canAttack) return false;
        if (player == null) return false;
        Collider2D collider2D = Physics2D.OverlapBox(transform.position+Vector3.up*0.5f, attackCheckBox, 0, LayerMask.GetMask("Player"));
        if (collider2D != null)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region 投掷炸弹的方法
    public void ThrowBomb(Vector2 speed)
    {
        ResourcesManager.Instance.LoadAsync<GameObject>("Prefabs/Enemy/Bomb", (obj) =>
        {
            obj.transform.position = (Vector2)transform.position + new Vector2(transform.localScale.x, 1.4f);
            obj.GetComponent<Rigidbody2D>().velocity = speed;
        });
    }
    #endregion

    #region 计算炸弹跟踪玩家所需要的初速度的方法
    public Vector2 GetThrowBombToPlayerSpeed(float time)
    {
        Vector2 throwPos = (Vector2)transform.position + new Vector2(transform.localScale.x, 0.7f);
        float horizontalSpeed = Mathf.Abs(player.transform.position.x - throwPos.x) / time * transform.localScale.x;
        //float verticalSpeed = Mathf.Abs(player.transform.position.y - throwPos.y) / time - (0.5f  *2* Physics2D.gravity.y * time);
        float verticalSpeed = (-2 * Physics2D.gravity.y * time / 2) * 0.7f;
        Vector2 throwSpeed = new Vector2(horizontalSpeed, verticalSpeed);
        return throwSpeed;
    }
    #endregion

    #region 检测是否在开关范围内的方法
    public void CheckSwitch()
    {
        Collider2D collider2D = Physics2D.OverlapBox(transform.position, Vector2.one, 0, LayerMask.GetMask("Switch"));
        if (collider2D == null) return;
        if (collider2D.TryGetComponent<ISwitch>(out ISwitch _switch))
        {
            _switch.CanTrigger(this);
        }
    }
    #endregion

    #region 让面朝向向玩家的方向转的方法
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
    #endregion

    #region 检测前方是否有障碍物或者墙壁的方法
    public bool CheckWallPatrol()
    {
        Vector2 checkPos = transform.position + new Vector3(transform.localScale.x * 0.65f, 0.5f, 0);
        Vector2 boxSize = new Vector2(0.5f, 0.85f);
        Collider2D collider2D = Physics2D.OverlapBox(checkPos, boxSize, 0, LayerMask.GetMask("Ground", "Obstacle","Tool","Spike"));
        if (collider2D != null)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region 水平翻转精灵图片的方法
    public void Filp()
    {
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    }
    #endregion

    #region 重置animatorTrigger的代码，防止出现动画切换bug
    public void AniSetTrigger(string triggerName)
    {
        if (triggerName == string.Empty) return;
        if (triggerName == previousTrigger) return;
        if (previousTrigger != string.Empty)
        {
            animator.ResetTrigger(previousTrigger);
        }
        previousTrigger = triggerName;
        animator.SetTrigger(previousTrigger);
    }
    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position+Vector3.up*0.5f,attackCheckBox);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(transform.localScale.x * 0.65f, 0.5f, 0),new Vector2(0.5f,0.85f));
    }
}
