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

    public override void EnterOperator()
    {
        animator.SetTrigger("EnterControl");
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

    public void InitState()
    {
        stateMachine = new StateMachine();
        idleState = new CrabIdle(stateMachine,this);
        onControllState = new CrabContro(stateMachine, this);
        stateMachine.Init(idleState);
    }

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

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        SetVelocity(inputX*moveSpeed);
        if(Mathf.Abs(rb2D.velocity.x) > 0)
        {
            animator.Play("Move");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
        //翻转精灵图片
        if(inputX>0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(inputX<0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public override void TakeDamge(float damage, UnityAction callback = null)
    {
        if(hasDead)return;
        health -= damage;
        if (health <= 0)
        {
            Dead();
        }
        callback?.Invoke();
    }

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

    public void CheckSwitch()
    {
        Collider2D collider2D = Physics2D.OverlapBox(transform.position, Vector2.one, 0, LayerMask.GetMask("Switch"));
        if (collider2D == null) return;
        if (collider2D.TryGetComponent<ISwitch>(out ISwitch _switch))
        {
            _switch.CanTrigger(this);
        }
    }


    public void SetVelocity(Vector2 speed)
    {
        rb2D.velocity = speed;
    }

    public void SetVelocity(float speedX,float speedY)
    {
        rb2D.velocity = new Vector2(speedX,speedY);
    }

    public void SetVelocity(float speedX)
    {
        rb2D.velocity = new Vector2(speedX,rb2D.velocity.y);
    }

    public void Init()
    {
        transform.position = new Vector3(46.5f, -6, 0);
        SetVelocity(0);
        ChangeState(CrabStateType.Idle);
    }

    public void OnPlayerDead()
    {
        ChangeState(CrabStateType.Idle);
    }
}
