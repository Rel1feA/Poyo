using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float moveSpeed;
    public bool isInTestState;
    private Player player;
    private Rigidbody2D rb2D;
    private Collider2D[] collider2Ds;
    private SpriteRenderer spriteRenderer;

    private bool isTestMove;

    private void Start()
    {
        player=GetComponent<Player>();
        rb2D=GetComponent<Rigidbody2D>();
        collider2Ds = GetComponents<Collider2D>();
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(isInTestState)
            {
                ExitTest();
            }
            else
            {
                EnterTest();
            }
            isInTestState = !isInTestState;
        }
        if(!isInTestState)
        {
            return;
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.M))
            {
                if(!isTestMove)
                {
                    Debug.Log("进入飞行模式");
                    isTestMove = true;
                    rb2D.velocity = Vector2.zero;
                    player.controller.enabled = false;
                    player.enabled = false;
                    rb2D.isKinematic = true;
                    for (int i = 0; i < collider2Ds.Length; i++)
                    {
                        collider2Ds[i].isTrigger = true;
                    }
                }
                else
                {
                    Debug.Log("退出飞行模式");
                    player.controller.enabled = true;
                    player.enabled = true;
                    rb2D.isKinematic = false;
                    for (int i = 0; i < collider2Ds.Length; i++)
                    {
                        collider2Ds[i].isTrigger = false;
                    }
                    isTestMove= false;
                }

            }
            if(isTestMove)
            {
                TestMove();
            }
            if(Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("执行死亡");
                player.TakeDamge(9999);
            }
        }
    }

    public void TestMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(moveX, moveY, 0);
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        if (moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void EnterTest()
    {
        Debug.Log("进入调试模式");
        player.currentHealth = 999;
        spriteRenderer.color = Color.red;
        isTestMove= false;
    }

    public void ExitTest()
    {
        Debug.Log("推出调试模式");
        spriteRenderer.color = Color.white;
        player.currentHealth = player.playerData.maxHealth;
        player.controller.enabled = true;
        player.enabled = true;
        rb2D.isKinematic = false;
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            collider2Ds[i].isTrigger = false;
        }
    }
}
