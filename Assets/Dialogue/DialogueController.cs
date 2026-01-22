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
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener<Dialogue>("StartDialogue", OnStartDialogue);
        EventCenter.Instance.RemoveListener<Dialogue>("ContinueDialogue", OnStartDialogue);
    }

    public void OnStartDialogue(Dialogue dialogue)
    {
        if(dialogue.dialogueObject==dialoguePanel.dialogueType)
        {
            dialoguePanel.gameObject.SetActive(true);
            dialoguePanel.UpdateText(dialogue);
        }
    }
}
