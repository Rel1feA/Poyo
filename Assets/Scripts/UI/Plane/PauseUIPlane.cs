using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIPlane : BasePanel
{
    private void Start()
    {
        GetControl<Button>("ResumeBTN").onClick.AddListener(() =>
        {
            GameManager.Instance.ResumeGame();
        });

        GetControl<Button>("MenuBTN").onClick.AddListener(() =>
        {
            GameManager.Instance.BackToMenu();
        });

        GetControl<Button>("SettingBTN").onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPlane>("SettingPlane",E_UI_Layer.System);
        });
    }
}
