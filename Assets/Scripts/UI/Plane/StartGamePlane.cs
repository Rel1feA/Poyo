using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartGamePlane : BasePanel
{

    private void Start()
    {
        InitBTNTXT();
    }

    #region 初始化面板的文字按钮
    public void InitBTNTXT()
    {
        UIManager.AddCustomEventListener(GetControl<Text>("StartTXT"), EventTriggerType.PointerEnter, (value) =>
        {
            GetControl<Text>("StartTXT").GetComponent<Animator>().SetTrigger("Bigger");
        });
        UIManager.AddCustomEventListener(GetControl<Text>("StartTXT"), EventTriggerType.PointerClick, (value) =>
        {
            GameManager.Instance.StartGame();
        });
        UIManager.AddCustomEventListener(GetControl<Text>("StartTXT"), EventTriggerType.PointerExit, (value) =>
        {
            GetControl<Text>("StartTXT").GetComponent<Animator>().SetTrigger("Normal");
        });
        UIManager.AddCustomEventListener(GetControl<Text>("SettingTXT"), EventTriggerType.PointerEnter, (value) =>
        {
            GetControl<Text>("SettingTXT").GetComponent<Animator>().SetTrigger("Bigger");
        });
        UIManager.AddCustomEventListener(GetControl<Text>("SettingTXT"), EventTriggerType.PointerClick, (value) =>
        {
            UIManager.Instance.ShowPanel<SettingPlane>("SettingPlane");
        });
        UIManager.AddCustomEventListener(GetControl<Text>("SettingTXT"), EventTriggerType.PointerExit, (value) =>
        {
            GetControl<Text>("SettingTXT").GetComponent<Animator>().SetTrigger("Normal");
        });
        UIManager.AddCustomEventListener(GetControl<Text>("QuitTXT"), EventTriggerType.PointerEnter, (value) =>
        {
            GetControl<Text>("QuitTXT").GetComponent<Animator>().SetTrigger("Bigger");
        });
        UIManager.AddCustomEventListener(GetControl<Text>("QuitTXT"), EventTriggerType.PointerClick, (value) =>
        {
            GameManager.Instance.QuitGame();
        });
        UIManager.AddCustomEventListener(GetControl<Text>("QuitTXT"), EventTriggerType.PointerExit, (value) =>
        {
            GetControl<Text>("QuitTXT").GetComponent<Animator>().SetTrigger("Normal");
        });
    } 
    #endregion
}
