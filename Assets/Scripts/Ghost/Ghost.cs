using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, IOperator
{
    public float moveSpeed;
    public float lifeTime;
    public Player player;
    private float timer;

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
    }


    public void ExitOperator()
    {
        
    }

    public void Move()
    {
        float moveX=Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(moveX, moveY, 0);
        transform.position += moveDir* moveSpeed*Time.deltaTime;
    }
}
