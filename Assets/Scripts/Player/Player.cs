using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IOperator
{
    public float moveSpeed;
    public float jumpHeight;
    public OperatorManager operatorManager;
    public Ghost ghost;

    private Rigidbody2D rb2D;

    private void Start()
    {
        rb2D= GetComponent<Rigidbody2D>();
        operatorManager.ChangeOperator(this);
    }

    public void Move()
    {
        float inputX = Input.GetAxis("Horizontal");
        rb2D.velocity=new Vector2(inputX*moveSpeed,rb2D.velocity.y);
    }

    public void Jump()
    {
        if(!CheckGround()) return;
        float speedY = Mathf.Sqrt((-Physics2D.gravity.y)*rb2D.gravityScale * 2 * jumpHeight);
        rb2D.velocity += Vector2.up * speedY;
    }

    public void UseSkill()
    {
        operatorManager.ChangeOperator(ghost);
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
}
