using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, IOperator
{
    public float moveSpeed;
    public float moveRadius;
    public Player player;
    public float CheckEnemyRadius;
    public bool canOperator { get; set; }

    private bool hasTrigger;

    private void OnEnable()
    {
        EventCenter.Instance.AddListener("PlayerDead", OnPlayerDead);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener("PlayerDead", OnPlayerDead);
    }

    private void Awake()
    {
        player=FindObjectOfType<Player>();
    }

    private void Start()
    {
        transform.localScale=player.transform.localScale;
        hasTrigger = false;
    }

    #region 实现操控接口的进入操控时所执行的方法
    public void EnterOperator()
    {
        canOperator = true;
    }
    #endregion

    #region 实现操控接口的在操控时每帧执行的方法
    public void OnOperator()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OperatorManager.Instance.ChangeOperator(player.controller);
            player.controller.useRaduisObj.SetActive(false);
            Destroy(gameObject);
        }
        Move();
    }
    #endregion

    #region 实现操控接口的退出操控所执行的方法
    public void ExitOperator()
    {
        Destroy(gameObject);
    }
    #endregion

    #region 移动的逻辑
    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 moveDistance = new Vector3(moveX, moveY, 0)*moveSpeed*Time.deltaTime;
        transform.position += moveDistance;
        if(Vector2.Distance(player.transform.position,transform.position)>moveRadius)
        {
            transform.position-=moveDistance;
        }
        if (moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    } 
    #endregion

    #region 检测敌人的方法（已弃用）
    //(这个检测敌人的方法已弃用，因为会出现可能检测不到的bug)
    public void CheckEnemy()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, CheckEnemyRadius, LayerMask.GetMask("Enemy", "Tool"));
        if (collider != null)
        {
            if (collider.TryGetComponent<IOperator>(out IOperator _operator))
            {
                player.controller.useRaduisObj.SetActive(false);
                OperatorManager.Instance.ChangeOperator(_operator);
            }
        }
    }
    #endregion

    #region 检测敌人的方法
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasTrigger) return;
        if (collision.TryGetComponent<IOperator>(out IOperator _operator))
        {
            player.controller.useRaduisObj.SetActive(false);
            OperatorManager.Instance.ChangeOperator(_operator);
            hasTrigger = true;
        }
    } 
    #endregion

    #region 监听玩家死亡时所执行的方法
    public void OnPlayerDead()
    {
        Destroy(gameObject);
    }
    #endregion

    #region 实现操控接口获取游戏物体本身
    public GameObject GetOperatorObject()
    {
        return gameObject;
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)player.transform.position, moveRadius);
    }
}
