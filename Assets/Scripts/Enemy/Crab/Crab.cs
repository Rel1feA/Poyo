using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crab : Enemy
{
    public StateMachine stateMachine;
    public CrabState idleState;
    public CrabState onControllState;
    public CrabState DeadState;

    [HideInInspector]
    public Rigidbody2D rb2D;
    [HideInInspector]
    public Player player;
    [HideInInspector]
    public Animator animator;
    private string previousTrigger;

    [Header("移动相关")]
    public float moveSpeed;

    [Header("基本属性")]
    public float maxHealth;
    private float health;

    private void OnEnable()
    {
        EventCenter.Instance.AddListener("PlayerDead", OnPlayerDead);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("PlayerDead", OnPlayerDead);
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        rb2D = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        health = maxHealth;
        InitState();
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

    #region 物体被销毁时所执行的方法
    private void OnDestroy()
    {
        if (OperatorManager.Instance.GetCurrentOperatorObj() != null)
        {
            if (OperatorManager.Instance.GetCurrentOperatorObj() == gameObject)
            {
                OperatorManager.Instance.ChangeOperator(player.controller);
            }
        }
    } 
    #endregion

    #region 实现操控接口所需要的方法
    public override void EnterOperator()
    {
        AniSetTrigger("EnterControl");
        canOperator = true;
    }

    public override void ExitOperator()
    {
        canOperator = false;
        SetVelocity(0);
    }

    public override GameObject GetOperatorObject()
    {
        return gameObject;
    } 
    #endregion

    #region 初始化状态的方法
    public void InitState()
    {
        stateMachine = new StateMachine();
        idleState = new CrabIdle(stateMachine, this);
        onControllState = new CrabContro(stateMachine, this);
        stateMachine.Init(idleState);
    } 
    #endregion

    #region 给外部调用改变状态的方法
    public void ChangeState(CrabStateType type)
    {
        switch (type)
        {
            case CrabStateType.Idle:
                stateMachine.Changestate(idleState);
                break;
            case CrabStateType.Control:
                stateMachine.Changestate(onControllState);
                break;
            default:
                break;
        }
    } 
    #endregion

    #region 移动方法
    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        SetVelocity(inputX * moveSpeed);
        if (Mathf.Abs(rb2D.velocity.x) > 0)
        {
            AniSetTrigger("Move");
        }
        else
        {
            AniSetTrigger("Idle");
        }
        //翻转精灵图片
        if (inputX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    } 
    #endregion

    #region 实现takedamage接口所执行的方法
    public override void TakeDamge(float damage, UnityAction callback = null)
    {
        if (hasDead) return;
        health -= damage;
        if (health <= 0)
        {
            Dead();
        }
        callback?.Invoke();
    } 
    #endregion

    #region 死亡的方法
    public void Dead()
    {
        if (OperatorManager.Instance.GetCurrentOperatorObj() != null)
        {
            if (OperatorManager.Instance.GetCurrentOperatorObj() == gameObject)
            {
                OperatorManager.Instance.ChangeOperator(player.controller);
            }
        }
        hasDead = true;
        GameObject.Destroy(gameObject);
    } 
    #endregion

    #region 检测是否碰到开关的方法
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

    #region 给外部调用设置速度的方法
    public void SetVelocity(Vector2 speed)
    {
        rb2D.velocity = speed;
    }

    public void SetVelocity(float speedX, float speedY)
    {
        rb2D.velocity = new Vector2(speedX, speedY);
    }

    public void SetVelocity(float speedX)
    {
        rb2D.velocity = new Vector2(speedX, rb2D.velocity.y);
    } 
    #endregion

    #region 重置animatorTrigger的代码，防止出现动画切换bug
    public void AniSetTrigger(string triggerName)
    {
        if (triggerName == string.Empty) return;
        if (triggerName == previousTrigger) return;
        if (previousTrigger != null)
        {
            animator.ResetTrigger(previousTrigger);
        }
        previousTrigger = triggerName;
        animator.SetTrigger(previousTrigger);
    }
    #endregion

    #region 监听Player死亡时执行的方法
    public void OnPlayerDead()
    {
        ChangeState(CrabStateType.Idle);
    } 
    #endregion
}
