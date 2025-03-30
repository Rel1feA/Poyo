using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour,IOperator,ITakeDamge,IClimb
{
    public PlayerData playerData;
    private float currentHealth;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float timer;

    public bool canOperator { get; set; }
    public bool IsClimb { get; set; }
    public bool CanClimb { get; set; }


    private void Start()
    {
        rb2D= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth=playerData.maxHealth;
        rb2D.gravityScale=playerData.normalGravityScale;
        OperatorManager.Instance.InitOperator(this);
    }

    private void Update()
    {
        CheckSwitch();
        ChangeGravityScale();
    }

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        rb2D.velocity = new Vector2(inputX * playerData.moveSpeed, rb2D.velocity.y);
        ChangeSpriteDir(inputX);
        if(inputX!=0)
        {
            animator.Play("Move");
        }
        else
        {
            animator.Play("Idle");
        }
    }

    public void Jump()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        float speedY = Mathf.Sqrt((-Physics2D.gravity.y)*rb2D.gravityScale * 2 * playerData.jumpHeight);
        rb2D.velocity += Vector2.up * speedY;
    }

    public void ChangeGravityScale()
    {
        if (IsClimb) return;
        if(!CheckGround()&&rb2D.velocity.y<0)
        {
            rb2D.gravityScale = playerData.fallGravityScale;
        }
        else
        {
            rb2D.gravityScale = playerData.normalGravityScale;
        }
    }

    public void UseSkill()
    {
        GameObject ghost = ResourcesManager.Instance.Load<GameObject>("Prefabs/ghost");
        ghost.transform.position = transform.position + new Vector3(transform.localScale.x, 1, 0);
        OperatorManager.Instance.ChangeOperator(ghost.GetComponent<Ghost>());
    }

    public bool CheckGround()
    {
       return Physics2D.OverlapBox(transform.position + new Vector3(transform.localScale.x * 0.1f, 0, 0), new Vector2(0.8f,0.3f), 0, LayerMask.GetMask("Ground","Tool"));
    }

    public void CheckSwitch()
    {
        Collider2D collider2D = Physics2D.OverlapBox(transform.position, Vector2.one, 0,LayerMask.GetMask("Switch"));
        if(collider2D == null) return;
        if (collider2D.TryGetComponent<ISwitch>(out ISwitch _switch))
        {
            _switch.CanTrigger(this);
        }
    }

    public void ChangeSpriteDir(float value)
    {
        if(value>0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(value<0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position +new Vector3(transform.localScale.x*0.1f,0,0), new Vector2(0.8f, 0.3f));
    }

    public void EnterOperator()
    {
        timer = 0;
        canOperator = true;
    }

    public void OnOperator()
    {
        //操作前腰（测试用）
        timer += Time.deltaTime;
        if(timer<playerData.operatorCoolTime)
        {
            spriteRenderer.color = Color.green;
            return;
        }
        //
        spriteRenderer.color = Color.white;
        //上楼梯，并且下面的移动等操作不执行
        if (CanClimb&&!IsClimb)
        {
            if(Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.S))
            {
                EnterClimb();
            }
        }
        if(IsClimb)
        {
            OnClimb();
            return;
        }
        //


        //移动
        Move();
        //

        //使用技能
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseSkill();
            rb2D.velocity = Vector2.zero;
        }
        //

        //跳跃
        if(Input.GetKeyDown(KeyCode.Space)&&CheckGround())
        {
            Jump();
        }
        //
    }

    public void ExitOperator()
    {
        animator.Play("Idle");
    }

    public void TakeDamge(float damage,UnityAction callback = null)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            EventCenter.Instance.EventTrigger("PlayerDead");
            OperatorManager.Instance.ChangeOperator(null);
            GameManager.Instance.ReStart();
            Destroy(gameObject);
        }
        callback?.Invoke();
    }

    public GameObject GetOperatorObject()
    {
        return gameObject;
    }

    public void EnterClimb()
    {
        IsClimb = true;
        rb2D.gravityScale = 0;
    }

    public void OnClimb()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        ChangeSpriteDir(inputX);
        Vector2 speed=new Vector2(inputX, inputY)*playerData.climbSpeed;
        rb2D.velocity = speed;
    }

    public void ExitClimb()
    {
        IsClimb = false;
        rb2D.gravityScale = playerData.normalGravityScale;
    }
}
