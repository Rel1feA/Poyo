using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float damge;
    private Animator animator;
    private PolygonCollider2D[] polygonCollider;
    private string previousTrigger;

    private void Awake()
    {
        polygonCollider = GetComponentsInChildren<PolygonCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<ITakeDamge>(out ITakeDamge takeDamge))
        {
            takeDamge.TakeDamge(damge);
        }
    }

    public void OnAnimUp()
    {
        for(int i = 0; i < polygonCollider.Length; i++)
        {
            polygonCollider[i].enabled = true;
        }
    }

    public void OnAnimDown()
    {
        for (int i = 0; i < polygonCollider.Length; i++)
        {
            polygonCollider[i].enabled = false;
        }
    }

    public void AniSetTrigger(string triggerName)
    {
        if (triggerName == string.Empty) return;
        if (previousTrigger != null)
        {
            animator.ResetTrigger(previousTrigger);
        }
        previousTrigger = triggerName;
        animator.SetTrigger(previousTrigger);
    }
}
