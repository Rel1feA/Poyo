using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : BasePanel
{
    [SerializeField]
    private Transform targetTra;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite rightImg;
    [SerializeField]
    private Sprite leftImg;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private float showTextSpeed;

    private Tween currentTween=null;

    public E_DialogueObject dialogueType;

    private void OnEnable()
    {
        InitPanel();
    }

    private void Update()
    {
        if(GameManager.Instance.currentState==GameStateType.Normal)
        {
            AudioManager.Instance.StopAudio("KeyBoard");
        }
    }

    public void InitPanel()
    {
        Transform parentTra=targetTra.parent;
        float faceDir=targetTra.localScale.x;
        if(parentTra != null )
        {
            faceDir*=parentTra.localScale.x;
        }
        transform.position=targetTra.position+new Vector3(faceDir*offset.x,offset.y,offset.z);
        if(faceDir>0)
        {
            image.sprite = rightImg;
            rectTransform.pivot = new Vector2(0, 0);
        }
        else
        {
            image.sprite = leftImg;
            rectTransform.pivot = new Vector2(1, 0);
        }
        ShowPanel();
    }

    public void UpdateText(Dialogue dialogue)
    {
        DialogueManager.Instance.SetCanContinue(false);
        GetControl<Text>("Text").text = string.Empty;
        AudioManager.Instance.PlayAudio("DialogueTip");
        AudioManager.Instance.PlayAudio("KeyBoard");
        currentTween =GetControl<Text>("Text").DOText(dialogue.content, showTextSpeed * dialogue.content.Length).SetEase(Ease.Linear).OnComplete(() =>
        {
            AudioManager.Instance.StopAudio("KeyBoard");
            DialogueManager.Instance.SetCanContinue(true);
        });
    }

    public void SkipDoText(Dialogue dialogue)
    {
        if (currentTween!=null&&currentTween.IsPlaying())
        {
            currentTween.Kill();
            currentTween=null;
            GetControl<Text>("Text").text = dialogue.content;
            AudioManager.Instance.StopAudio("KeyBoard");
            DialogueManager.Instance.SetCanContinue(true);
        }
    }

    public void ShowPanel()
    {
        rectTransform.DOScale(Vector3.one*0.01f, 0.3f).From(Vector3.zero);
    }

    public void HidePanel()
    {
        rectTransform.DOScale(Vector3.zero, 0.3f).From(Vector3.one * 0.01f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void OnEndDialogue(Dialogue dialogue)
    {
        HidePanel();
    }
}
