using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float upVel;
    private bool shouldMove;
    private Vector2 realVel;

    private void Start()
    {
        rb2D=GetComponent<Rigidbody2D>();
    }

    public void StartMoving(Vector2 dir)
    {
        realVel = upVel * dir;
        shouldMove= true;
    }

    public void StopMoving()
    {
        rb2D.velocity = Vector2.zero;
        shouldMove= false;
    }

    private void FixedUpdate()
    {
        if(shouldMove)
        {
            rb2D.velocity = realVel;
        }
    }
}
