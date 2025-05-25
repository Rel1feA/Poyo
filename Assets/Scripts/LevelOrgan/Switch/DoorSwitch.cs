using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch :MonoBehaviour
{
    public List<AutoDoor> autoDoors;
    public Vector2 forceDir;
    private bool isDown;
    private Animator animator;

    private void Start()
    {
        animator= GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        bool currentPressed = hasColliderOn();
        if(currentPressed!=isDown)
        {
            foreach(AutoDoor autoDoor in autoDoors)
            {
                if(currentPressed)
                {
                    autoDoor.StartMoving(forceDir);
                    animator.Play("Down");
                }
                else
                {
                    autoDoor.StopMoving();
                    animator.Play("Up");
                }
            }
        }
        isDown=currentPressed;
    }


    public bool hasColliderOn()
    {
        Collider2D[] collider2Ds= Physics2D.OverlapBoxAll(transform.position + new Vector3(0, 0.4f, 0),new Vector2(1f,0.7f), 0, LayerMask.GetMask("Player", "Enemy", "Tool"));
        if(collider2Ds.Length > 0 )
        {
            return true;
        }
        return false;
    }

    public void PlayMusic(string name)
    {
        AudioManager.Instance.PlayAudio(name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 0.4f, 0), new Vector2(1f,0.7f));
    }
}
