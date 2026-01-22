using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class StartGamePlane : BasePanel
{

    private void Start()
    {
        InitBTN();
    }

    

    public void InitBTN()
    {
        if(SaveManager.Instance.GetUserData("A").isFirstOpenGame)
        {
            GetControl<Button>("StartBTN").interactable = false;
            GetControl<Text>("StartTXT").color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            GetControl<Button>("StartBTN").interactable = true;
            GetControl<Text>("StartTXT").color = new Color(1, 1, 1, 1f);
        }
        GetControl<Button>("StartBTN").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayAudio("UIClick");
            GameManager.Instance.StartGame();
        });
        UIManager.AddCustomEventListener(GetControl<Button>("StartBTN"), EventTriggerType.PointerEnter, (value) =>
        {
            AudioManager.Instance.PlayAudio("UISelect");
            GetControl<Button>("StartBTN").transform.DOScale(Vector3.one * 1.15f, 0.2f).From(Vector3.one);
        });
        UIManager.AddCustomEventListener(GetControl<Button>("StartBTN"), EventTriggerType.PointerExit, (value) =>
        {
            GetControl<Button>("StartBTN").transform.DOScale(Vector3.one, 0.2f).From(Vector3.one*1.15f);
        });

        GetControl<Button>("NewGameBTN").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayAudio("UIClick");
            GameManager.Instance.StartNewGame();
        });
        UIManager.AddCustomEventListener(GetControl<Button>("NewGameBTN"), EventTriggerType.PointerEnter, (value) =>
        {
            AudioManager.Instance.PlayAudio("UISelect");
            GetControl<Button>("NewGameBTN").transform.DOScale(Vector3.one * 1.15f, 0.2f).From(Vector3.one);
        });
        UIManager.AddCustomEventListener(GetControl<Button>("NewGameBTN"), EventTriggerType.PointerExit, (value) =>
        {
            GetControl<Button>("NewGameBTN").transform.DOScale(Vector3.one, 0.2f).From(Vector3.one * 1.15f);
        });

        GetControl<Button>("SettingBTN").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayAudio("UIClick");
            UIManager.Instance.ShowPanel<SettingPlane>("SettingPlane");
        });
        UIManager.AddCustomEventListener(GetControl<Button>("SettingBTN"), EventTriggerType.PointerEnter, (value) =>
        {
            AudioManager.Instance.PlayAudio("UISelect");
            GetControl<Button>("SettingBTN").transform.DOScale(Vector3.one * 1.15f, 0.2f).From(Vector3.one);
        });
        UIManager.AddCustomEventListener(GetControl<Button>("SettingBTN"), EventTriggerType.PointerExit, (value) =>
        {
            GetControl<Button>("SettingBTN").transform.DOScale(Vector3.one, 0.2f).From(Vector3.one * 1.15f);
        });

        GetControl<Button>("QuitBTN").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayAudio("UIClick");
            GameManager.Instance.QuitGame();
        });
        UIManager.AddCustomEventListener(GetControl<Button>("QuitBTN"), EventTriggerType.PointerEnter, (value) =>
        {
            AudioManager.Instance.PlayAudio("UISelect");
            GetControl<Button>("QuitBTN").transform.DOScale(Vector3.one * 1.15f, 0.2f).From(Vector3.one);
        });
        UIManager.AddCustomEventListener(GetControl<Button>("QuitBTN"), EventTriggerType.PointerExit, (value) =>
        {
            GetControl<Button>("QuitBTN").transform.DOScale(Vector3.one, 0.2f).From(Vector3.one * 1.15f);
        });


        GetControl<Button>("ChooseLevelBTN").onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayAudio("UIClick");
            UIManager.Instance.ShowPanel<ChooseLevelPlane>("ChooseLevelPlane");
        });
        UIManager.AddCustomEventListener(GetControl<Button>("ChooseLevelBTN"), EventTriggerType.PointerEnter, (value) =>
        {
            AudioManager.Instance.PlayAudio("UISelect");
            GetControl<Button>("ChooseLevelBTN").transform.DOScale(Vector3.one * 1.15f, 0.2f).From(Vector3.one);
        });
        UIManager.AddCustomEventListener(GetControl<Button>("ChooseLevelBTN"), EventTriggerType.PointerExit, (value) =>
        {
            GetControl<Button>("ChooseLevelBTN").transform.DOScale(Vector3.one, 0.2f).From(Vector3.one * 1.15f);
        });
    }
}
