using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public float upForce;

    private void Start()
    {
        rb2D=GetComponent<Rigidbody2D>();
    }

    public void AddForceToDoor(Vector2 dir)
    {
        rb2D.AddForce(dir*upForce*Time.deltaTime,ForceMode2D.Impulse);
    }
}
