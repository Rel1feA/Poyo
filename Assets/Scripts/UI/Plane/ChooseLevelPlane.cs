using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelPlane : BasePanel
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.DOScale(Vector3.one, 0.2f).From(Vector3.zero).SetUpdate(true);

        GetControl<Button>("Level1").onClick.AddListener(() => {
            AudioManager.Instance.PlayAudio("UIClick");
            UIManager.Instance.HidePanel("ChooseLevelPlane");
            LoadSceneManager.Instance.CreateTransferEffect(TransferEffectType.Fade, () =>
            {
                LevelManager.Instance.LoadLevel(1, null, (value) =>
                {
                    LoadSceneManager.Instance.ChangeEffect();
                    GameManager.Instance.ChangeState(GameStateType.OnGoing);
                });
            });
        });

        GetControl<Button>("Level2").onClick.AddListener(() => {
            AudioManager.Instance.PlayAudio("UIClick");
            UIManager.Instance.HidePanel("ChooseLevelPlane");
            LoadSceneManager.Instance.CreateTransferEffect(TransferEffectType.Fade, () =>
            {
                LevelManager.Instance.LoadLevel(2, null, (value) =>
                {
                    LoadSceneManager.Instance.ChangeEffect();
                    GameManager.Instance.ChangeState(GameStateType.OnGoing);
                });
            });
        });

        GetControl<Button>("Level3").onClick.AddListener(() => {
            AudioManager.Instance.PlayAudio("UIClick");
            UIManager.Instance.HidePanel("ChooseLevelPlane");
            LoadSceneManager.Instance.CreateTransferEffect(TransferEffectType.Fade, () =>
            {
                LevelManager.Instance.LoadLevel(3, null, (value) =>
                {
                    LoadSceneManager.Instance.ChangeEffect();
                    GameManager.Instance.ChangeState(GameStateType.OnGoing);
                });
            });
        });

        GetControl<Button>("BackBTN").onClick.AddListener(() => {
            AudioManager.Instance.PlayAudio("UIClick");
            UIManager.Instance.HidePanel("ChooseLevelPlane");
        });

        UpdateLevelBTN();
    }

    public void UpdateLevelBTN()
    {
        switch(GameManager.Instance.checkPointData.maxFinishLevel)
        {
            case 0:
            case 1:
                GetControl<Button>("Level1").interactable = true;
                GetControl<Button>("Level2").interactable = false;
                GetControl<Button>("Level3").interactable = false;
                break;
            case 2:
                GetControl<Button>("Level1").interactable = true;
                GetControl<Button>("Level2").interactable = true;
                GetControl<Button>("Level3").interactable = false;
                break;
            case 3:
                GetControl<Button>("Level1").interactable = true;
                GetControl<Button>("Level2").interactable = true;
                GetControl<Button>("Level3").interactable = true;
                break;
            default:
                break;
        }
    }

    public override void HideMe()
    {
        rectTransform.DOScale(Vector3.zero, 0.2f).From(Vector3.one).SetUpdate(true).OnComplete(() => { base.HideMe(); });
    }
}
