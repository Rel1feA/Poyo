using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemUIPlane : BasePanel
{
    private void Start()
    {
        GetControl<Button>("PauseBTN").onClick.AddListener(() =>
        {
            GameManager.Instance.PauseGame();
        });

        GetControl<Button>("ResetBTN").onClick.AddListener(() =>
        {
            Debug.Log("click");
            EventCenter.Instance.EventTrigger("Reset");
        });
    }
}
