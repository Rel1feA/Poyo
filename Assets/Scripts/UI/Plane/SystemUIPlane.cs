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
            AudioManager.Instance.PlayAudio("UIClick");
        });

        GetControl<Button>("ResetBTN").onClick.AddListener(() =>
        {
            EventCenter.Instance.EventTrigger("Reset");
            AudioManager.Instance.PlayAudio("UIClick");
        });
    }
}
