using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSwitch : MonoBehaviour,ISwitch
{
    public List<Spike> firstSpikes;
    public List<Spike> secondSpikes;
    private bool isOpen;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        OnClose();
    }

    public void OnOpen()
    {
        foreach (Spike spike in firstSpikes)
        {
            //spike.gameObject.SetActive(false);
            spike.AniSetTrigger("Down");
        }
        foreach(Spike spike in secondSpikes)
        {
            //spike.gameObject.SetActive(true);
            spike.AniSetTrigger("Up");
        }
        isOpen = true;
        animator.SetTrigger("Open");
    }


    public void OnClose()
    {
        foreach (Spike spike in firstSpikes)
        {
            //spike.gameObject.SetActive(true);
            spike.AniSetTrigger("Up");

        }
        foreach(Spike spike in secondSpikes)
        {
            //spike.gameObject.SetActive(false);
            spike.AniSetTrigger("Down");
        }
        isOpen = false;
        animator.SetTrigger("Close");
    }

    public void CanTrigger(IOperator _operator)
    {
        if (OperatorManager.Instance.currentOperator != _operator) return;
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (isOpen)
            {
                OnClose ();
            }
            else
            {
                OnOpen ();
            }
        }
    }
}
