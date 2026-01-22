using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShowTextPanel : BasePanel
{
    [TextArea]
    public List<string> text_list = new List<string>();

    [SerializeField]
    private float showTextSpeed;
    private int currentIndex;
    private Sequence sequence;

    private void Start()
    {
        currentIndex = 0;
        sequence = DOTween.Sequence();
        StartShowText();
    }

    public void StartShowText()
    {
        while (currentIndex < text_list.Count)
        {
            sequence.AppendCallback(() =>
            {
                AudioManager.Instance.PlayAudio("KeyBoard");
            });
            sequence.Append(GetControl<Text>("Text").DOText(text_list[currentIndex], showTextSpeed * text_list[currentIndex].Length).SetEase(Ease.Linear));
            sequence.AppendCallback(() =>
            {
                AudioManager.Instance.StopAudio("KeyBoard");
            });
            sequence.AppendInterval(3);
            sequence.AppendCallback(() =>
            {
                GetControl<Text>("Text").text=string.Empty;
            });
            currentIndex++;
        }
        sequence.AppendCallback(() =>
        {
            LoadSceneManager.Instance.CreateTransferEffect(TransferEffectType.Fade, () =>
            {
                LevelManager.Instance.LoadLevel("Beach", null, (value) =>
                {
                    LoadSceneManager.Instance.ChangeEffect();
                    GameManager.Instance.ChangeState(GameStateType.OnGoing);
                    UIManager.Instance.HidePanel("ShowTextPanel_1");
                });
            });
        });
    }
}
