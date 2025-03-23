using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Turtle : Enemy
{
    public StateMachine stateMachine;
    public TurtleState idleState;
    public TurtleState onControllState;
    public TurtleState DeadState;

    [HideInInspector]
    public Rigidbody2D rb2D;
    [HideInInspector]
    public Player player;

    [Header("移动相关")]
    public float moveSpeed;

    [Header("基本属性")]
    public float maxHealth;
    private float health;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        rb2D = GetComponent<Rigidbody2D>();
        health = maxHealth;
        InitState();
    }

    private void Update()
    {
        stateMachine.currentState.FrameUpdate();
        CheckSwitch();
    }

    public override void EnterOperator()
    {
        stateMachine.Changestate(onControllState);
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
        idleState = new TurtleIdle(stateMachine,this);
        onControllState = new TurtleContro(stateMachine, this);
        stateMachine.Init(idleState);
    }

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        SetVelocity(inputX*moveSpeed);
    }

    public override void TakeDamge(float damage, UnityAction callback = null)
    {
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

}
