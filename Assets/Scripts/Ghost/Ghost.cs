using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour, IOperator
{
    public float moveSpeed;
    public OperatorManager manager;

    public void Jump()
    {
        
    }

    public void Move()
    {
        float moveX=Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(moveX, moveY, 0);
        transform.position += moveDir* moveSpeed*Time.deltaTime;
    }

    public void UseSkill()
    {
        Debug.Log("UseSkill");
    }
}
