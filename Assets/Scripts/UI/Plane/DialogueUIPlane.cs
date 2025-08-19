using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUIPlane : BasePanel
{
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        GetControl<Text>("Text").text = Dialoguemanager.Instance.DisplayDialogue();
        rectTransform.DOScale(Vector3.one, 0.25f).From(Vector3.zero);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            string content=Dialoguemanager.Instance.DisplayDialogue();
            if(content!=string.Empty)
            {
                GetControl<Text>("Text").text = content;
            }
        }
    }

    public override void HideMe()
    {
        rectTransform.DOScale(Vector3.zero, 0.25f).From(Vector3.one).OnComplete(() => { base.HideMe(); });
    }
}
