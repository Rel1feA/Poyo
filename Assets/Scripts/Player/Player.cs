using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour,IOperator,ITakeDamge
{
    public PlayerData playerData;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D= GetComponent<Rigidbody2D>();
        OperatorManager.Instance.ChangeOperator(this);
    }

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        rb2D.velocity=new Vector2(inputX*playerData.moveSpeed,rb2D.velocity.y);
    }

    public void Jump()
    {
        float speedY = Mathf.Sqrt((-Physics2D.gravity.y)*rb2D.gravityScale * 2 * playerData.jumpHeight);
        rb2D.velocity += Vector2.up * speedY;
    }

    public void UseSkill()
    {
        GameObject ghost = ResourcesManager.Instance.Load<GameObject>("Prefabs/ghost");
        ghost.transform.position=transform.position;
        OperatorManager.Instance.ChangeOperator(ghost.GetComponent<Ghost>());
    }

    public bool CheckGround()
    {
       return Physics2D.OverlapBox(transform.position-Vector3.up*0.25f, Vector2.one, 0, LayerMask.GetMask("Ground"));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position - Vector3.up * 0.25f, Vector2.one);
    }

    public void EnterOperator()
    {
        
    }

    public void OnOperator()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.E))
        {
            UseSkill();
        }
        if(Input.GetKeyDown(KeyCode.Space)&&CheckGround())
        {
            Jump();
        }
    }

    public void ExitOperator()
    {
        
    }

    public void TakeDamge(UnityAction callback = null)
    {
        callback?.Invoke();
    }
}
