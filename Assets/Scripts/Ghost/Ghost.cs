using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, IOperator
{
    public float moveSpeed;
    public float lifeTime;
    public Player player;
    private float timer;

    public float CheckEnemyRadius;

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

    public void EnterOperator()
    {
        timer = 0;
    }

    public void OnOperator()
    {
        timer += Time.deltaTime;
        if( timer > lifeTime )
        {
            OperatorManager.Instance.ChangeOperator(player);
            Destroy(gameObject);
        }
        Move();
        CheckEnemy();
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
    }
    
    public void CheckEnemy()
    {
        Collider2D collider= Physics2D.OverlapCircle(transform.position, CheckEnemyRadius,LayerMask.GetMask("Enemy"));
        if (collider!=null)
        {
            if (collider.TryGetComponent<IOperator>(out IOperator _operator))
            {
                OperatorManager.Instance.ChangeOperator(_operator);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, CheckEnemyRadius);
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
