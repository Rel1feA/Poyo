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
    }

    public void EnterOperator()
    {
        canOperator=true;
    }

    public void OnOperator()
    {
        if( Vector2.Distance(transform.position,player.transform.position)>moveRadius||Input.GetKeyDown(KeyCode.Q) )
        {
            OperatorManager.Instance.ChangeOperator(player.controller);
            player.controller.useRaduisObj.SetActive(false);
            Destroy(gameObject);
        }
        Move();
        //CheckEnemy();
    }


    public void ExitOperator()
    {
        Destroy(gameObject);
    }

    public void Move()
    {
        float moveX=Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(moveX, moveY, 0);
        transform.position += moveDir* moveSpeed*Time.deltaTime;
        if(moveX>0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(moveX<0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    //弃用的检测敌人方法(因为有时候会出现检测不到敌人的bug)
    public void CheckEnemy()
    {
        Collider2D collider= Physics2D.OverlapCircle(transform.position, CheckEnemyRadius,LayerMask.GetMask("Enemy","Tool"));
        if (collider!=null)
        {
            if (collider.TryGetComponent<IOperator>(out IOperator _operator))
            {
                player.controller.useRaduisObj.SetActive(false);
                OperatorManager.Instance.ChangeOperator(_operator);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IOperator>(out IOperator _operator))
        {
            player.controller.useRaduisObj.SetActive(false);
            OperatorManager.Instance.ChangeOperator(_operator);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.blue;
        Gizmos.DrawWireSphere((Vector2)player.transform.position, moveRadius);
    }

    public void OnPlayerDead()
    {
        Destroy(gameObject);
    }

    public GameObject GetOperatorObject()
    {
        return gameObject;
    }
}
