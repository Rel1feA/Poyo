using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTipPlane : BasePanel
{
    public float showDuration;
    public float fadeDuration;
    public float moveDistance;
    public Vector3 originalPosition;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        ShowEffect();
    }

    public void ShowEffect()
    {
        // 重置位置和状态
        rectTransform.anchoredPosition = originalPosition;
        canvasGroup.alpha = 0;

        // 创建动画序列
        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
        // 第一阶段：淡入+上浮
        seq.Append(canvasGroup.DOFade(1, fadeDuration));
        seq.Join(rectTransform.DOAnchorPosY(originalPosition.y + moveDistance, fadeDuration));

        // 第二阶段：保持可见
        seq.AppendInterval(showDuration - (fadeDuration * 2));

        // 第三阶段：淡出
        seq.Append(canvasGroup.DOFade(0, fadeDuration));

        // 完成后隐藏面板
        seq.OnComplete(() => { UIManager.Instance.HidePanel("SaveTipPlane"); });
    }
}
