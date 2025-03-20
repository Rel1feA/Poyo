using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSwitch : MonoBehaviour,ISwitch
{
    public List<Spike> firstSpikes;
    public List<Spike> secondSpikes;
    private bool isOpen;

    private void Start()
    {
        OnClose();
    }

    public void OnOpen()
    {
        foreach (Spike spike in firstSpikes)
        {
            spike.gameObject.SetActive(false);
        }
        foreach(Spike spike in secondSpikes)
        {
            spike.gameObject.SetActive(true);
        }
        isOpen = true;
    }


    public void OnClose()
    {
        foreach (Spike spike in firstSpikes)
        {
            spike.gameObject.SetActive(true);
        }
        foreach(Spike spike in secondSpikes)
        {
            spike.gameObject.SetActive(false);
        }
        isOpen = false;
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
