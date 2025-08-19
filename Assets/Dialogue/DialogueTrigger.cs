using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueContent content;

    public void TriggerDialogue()
    {
        Dialoguemanager.Instance.StartDialogue(content);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }
}
