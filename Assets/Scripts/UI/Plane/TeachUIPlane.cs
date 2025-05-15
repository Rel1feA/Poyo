using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachUIPlane : BasePanel
{
    private Animator animator;

    private void Start()
    {
        animator=GetComponent<Animator>();
    }

    public override void HideMe()
    {
        animator.SetTrigger("Close");
    }

    public void OnAniCompleted()
    {
        Destroy(gameObject);
    }
}
