using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIPlane : BasePanel
{

    private void Start()
    {
        InitBTN();
    }

    public void InitBTN()
    {
        GetControl<Button>("ResumeBTN").onClick.AddListener(() =>
        {
            GameManager.Instance.ResumeGame();
            AudioManager.Instance.PlayAudio("UIClick");
        });

        GetControl<Button>("MenuBTN").onClick.AddListener(() =>
        {
            GameManager.Instance.BackToMenu();
            AudioManager.Instance.PlayAudio("UIClick");
        });

        GetControl<Button>("SettingBTN").onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPlane>("SettingPlane", E_UI_Layer.System);
            AudioManager.Instance.PlayAudio("UIClick");
        });
    }
}
