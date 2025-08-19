using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ProducerPlane : BasePanel
{
    public CanvasGroup canvasGroup;
    public float fadeDuration;
    public float textMoveDuration;
    public float textMoveDistance;
    public RectTransform textRect;
    private RectTransform rectTra;

    private void Start()
    {
        rectTra=GetComponent<RectTransform>();
        Sequence quenen = DOTween.Sequence();
        quenen.AppendInterval(1f);
        quenen.Append(canvasGroup.DOFade(1f, fadeDuration).From(0));
        quenen.Append(textRect.DOAnchorPosY(textRect.anchoredPosition.y+textMoveDistance, textMoveDuration));
        quenen.AppendInterval(3f);
        quenen.AppendCallback(() => {
            LoadSceneManager.Instance.CreateTransferEffect(TransferEffectType.Fade, () =>
            {
                LoadSceneManager.Instance.LoadSceneAsync("StartGame", null, (value) =>
                {
                    LoadSceneManager.Instance.ChangeEffect();
                });
            });
            GameManager.Instance.ChangeState(GameStateType.Normal);
        });
    }
}
