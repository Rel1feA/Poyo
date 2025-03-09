using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSwitch : MonoBehaviour,ISwitch
{
    public Spike[] targetSpikes;
    private bool isOpen;

    private void Start()
    {
        OnClose();
    }

    public void OnOpen()
    {
        foreach (Spike spike in targetSpikes)
        {
            spike.gameObject.SetActive(false);
        }
        isOpen = true;
    }


    public void OnClose()
    {
        foreach (Spike spike in targetSpikes)
        {
            spike.gameObject.SetActive(true);
        }
        isOpen = false;
    }

    public void Trigger()
    {
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
