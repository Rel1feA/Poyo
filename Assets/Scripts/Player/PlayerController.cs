using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour,IOperator,IClimb
{
    public PlayerData playerData;

    public Rigidbody2D rb2D;
    private Animator animator;
    private string previousTrigger;
    private Player player;

    //private float timer;
    public float inputX;
    public GameObject useRaduisObj;
    public bool isOnWake;//判断是否在前腰阶段的布尔值

    public bool canOperator { get; set; }
    public bool IsClimb { get; set; }
    public bool CanClimb { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        player =GetComponent<Player>();
        rb2D= GetComponent<Rigidbody2D>();
        rb2D.gravityScale=playerData.normalGravityScale;
        OperatorManager.Instance.InitOperator(this);
        useRaduisObj.SetActive(false);
    }

    #region 操控移动代码
    public void Move()
    {
        inputX = Input.GetAxis("Horizontal");
        Vector2 speed = new Vector2(inputX * playerData.moveSpeed, rb2D.velocity.y);
        SetVel(speed);
        ChangeSpriteDir(inputX);
    }
    #endregion

    #region 跳跃代码
    public void Jump()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        float speedY = Mathf.Sqrt((-Physics2D.gravity.y) * rb2D.gravityScale * 2 * playerData.jumpHeight);
        rb2D.velocity += Vector2.up * speedY;
        AudioManager.Instance.PlayAudio("player_jump");
    }
    #endregion

    #region 改变重力比例大小代码
    public void ChangeGravityScale()
    {
        if (IsClimb) return;
        if (!CheckGround() && rb2D.velocity.y < 0)
        {
            rb2D.gravityScale = playerData.fallGravityScale;
        }
        else
        {
            rb2D.gravityScale = playerData.normalGravityScale;
        }
    }
    #endregion

    #region 使用能力代码
    public void UseSkill()
    {
        GameObject ghost = ResourcesManager.Instance.Load<GameObject>("Prefabs/ghost");
        Vector3 offset = new Vector3(playerData.generateGhostPos.x * player.transform.localScale.x, playerData.generateGhostPos.y, playerData.generateGhostPos.z);
        ghost.transform.position = transform.position+offset;
        OperatorManager.Instance.ChangeOperator(ghost.GetComponent<Ghost>());
        player.ChangeState(PlayerStateType.Idle);
        useRaduisObj.SetActive(true);
    }
    #endregion

    #region 检测地面代码
    public bool CheckGround()
    {
        return Physics2D.OverlapBox(transform.position + new Vector3(transform.localScale.x * 0.07f, 0, 0), new Vector2(0.8f, 0.3f), 0, LayerMask.GetMask("Ground", "Tool","Obstacle"));
    }
    #endregion

    #region 检测是否碰到机关开关代码
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

    #region 改变精灵图像方向代码
    public void ChangeSpriteDir(float value)
    {
        if (value > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (value < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    } 
    #endregion

    #region 进入操控状态时代码
    public void EnterOperator()
    {
        //timer = 0;
        canOperator = true;
        player.ChangeState(PlayerStateType.Operator);
    }
    #endregion

    #region 实现操控接口每帧执行的逻辑（目前无方法在里面，转为在操控状态的每帧方法执行）
    public void OnOperator()
    {

    } 
    #endregion

    #region 在操控状态时每帧执行的逻辑
    public void BeOnOperator()
    {
        if (isOnWake) return;
        //操作前腰（测试用）
        //timer += Time.deltaTime;
        //if (timer < playerData.operatorCoolTime)
        //{
        //    spriteRenderer.color = Color.green;
        //    return;
        //}
        //

        //上楼梯，并且下面的移动等操作不执行
        if (CanClimb && !IsClimb)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                EnterClimb();
            }
        }
        if (IsClimb)
        {
            OnClimb();
            return;
        }
        //

        //使用技能
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseSkill();
            SetVel(0);
        }
        //

        //设置动画
        SetNormalAni();
        //

        //移动
        Move();
        //

        //跳跃
        if (Input.GetKeyDown(KeyCode.Space) && CheckGround())
        {
            Jump();
        }
        //
    }
    #endregion

    #region 退出被操控状态时代码
    public void ExitOperator()
    {
        inputX = 0;
        SetVel(0);
        player.ChangeState(PlayerStateType.Idle);
    }
    #endregion

    #region 实现操控接口，便于获取操控对象的gameobject代码
    public GameObject GetOperatorObject()
    {
        if(this!=null)
        {
            return gameObject;
        }
        return null;
    }
    #endregion

    #region 进入攀爬状态代码
    public void EnterClimb()
    {
        IsClimb = true;
        rb2D.gravityScale = 0;
        AniSetTrigger("Climb");
    }
    #endregion

    #region 在攀爬状态时每帧执行的代码
    public void OnClimb()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        ChangeSpriteDir(inputX);
        Vector2 speed = new Vector2(inputX, inputY) * playerData.climbSpeed;
        rb2D.velocity = speed;
        if(inputX==0&&inputY==0)
        {
            animator.speed = 0;
        }
        else
        {
            animator.speed = 1;
        }
    }
    #endregion

    #region 退出攀爬状态代码
    public void ExitClimb()
    {
        animator.speed = 1;
        IsClimb = false;
        rb2D.gravityScale = playerData.normalGravityScale;
    }
    #endregion

    #region 设置刚体速度代码
    public void SetVel(Vector2 newVel)
    {
        rb2D.velocity = newVel;
    } 


    public void SetVel(float newVelX)
    {
        rb2D.velocity=new Vector2(newVelX, rb2D.velocity.y);
    }
    #endregion

    #region 在被操控时正常状态下动画切换逻辑（排除攀爬动画）
    public void SetNormalAni()
    {
        if (inputX != 0 && CheckGround())
        {
            AniSetTrigger("Move");
            animator.SetBool("IsDown", true);
        }
        else if (!CheckGround())
        {
            animator.SetBool("IsDown", false);
            if (rb2D.velocity.y > 0)
            {
                AniSetTrigger("Jump");
            }
            else
            {
                AniSetTrigger("Fall");
            }
        }
        else
        {
            AniSetTrigger("Idle");
            animator.SetBool("IsDown", true);
        }
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

    #region 结束前摇执行的方法，给Wake动画片段使用
    public void EndWakeUp()
    {
        isOnWake = false;
    } 
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(transform.localScale.x * 0.07f, 0, 0), new Vector2(0.8f, 0.3f));
    }
}
