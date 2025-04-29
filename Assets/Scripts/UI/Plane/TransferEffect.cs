using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TransferEffect : BasePanel
{
    public UnityAction callback;
    private Animator animator;
    public string effectName;

    private void Awake()
    {
        animator=GetComponent<Animator>();
    }

    #region 执行完过渡效果后执行的逻辑
    public void CompleteEffect()
    {
        callback?.Invoke();
    } 
    #endregion

    public void ChangeEffect()
    {
        animator.SetTrigger("Change");
    }

    public void HideSelf()
    {
        UIManager.Instance.HidePanel("TransferEffects/"+effectName);
    }
}
