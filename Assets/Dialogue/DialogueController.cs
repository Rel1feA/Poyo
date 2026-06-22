using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    private DialoguePanel dialoguePanel;

    private void OnEnable()
    {
        EventCenter.Instance.AddListener<Dialogue>("StartDialogue", OnStartDialogue);
        EventCenter.Instance.AddListener<Dialogue>("ContinueDialogue", OnStartDialogue);
        EventCenter.Instance.AddListener<Dialogue>("EndDialogue", OnEndDialogue);
        EventCenter.Instance.AddListener<Dialogue>("SkipDialogue", SkipDoText);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener<Dialogue>("StartDialogue", OnStartDialogue);
        EventCenter.Instance.RemoveListener<Dialogue>("ContinueDialogue", OnStartDialogue);
        EventCenter.Instance.RemoveListener<Dialogue>("EndDialogue", OnEndDialogue);
        EventCenter.Instance.RemoveListener<Dialogue>("SkipDialogue", SkipDoText);
    }

    public void OnStartDialogue(Dialogue dialogue)
    {
        if(dialogue.dialogueObject==dialoguePanel.dialogueType)
        {
            if(!dialoguePanel.gameObject.activeSelf)
            {
                dialoguePanel.gameObject.SetActive(true);
            }
            dialoguePanel.UpdateText(dialogue);
        }
        else
        {
            dialoguePanel.HidePanel();
        }
    }

    public void OnEndDialogue(Dialogue dialogue)
    {
        dialoguePanel.OnEndDialogue(dialogue);
    }

    public void SkipDoText(Dialogue dialogue)
    {
        dialoguePanel.SkipDoText(dialogue);
    }
}
