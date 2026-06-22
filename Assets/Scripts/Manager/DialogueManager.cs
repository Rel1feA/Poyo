using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField]
    private List<DialogueGroup> dialogueGroup_list;

    private DialogueGroup currentDialogueGroup;
    private int currentDialogueIndex;
    private bool canContinue;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        canContinue = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)&&currentDialogueGroup!=null)
        {
            if(canContinue)
            {
                ContinueDialogue();
            }
            else
            {
                EventCenter.Instance.EventTrigger<Dialogue>("SkipDialogue", currentDialogueGroup.dialogue_list[currentDialogueIndex]);
            }
        }
    }

    public void StartDialogue(int id)
    {
        DialogueGroup targetDialogue=GetDialogueGroupById(id);
        if(targetDialogue != null)
        {
            currentDialogueGroup = targetDialogue;
            currentDialogueIndex = 0;
            canContinue = true;
            EventCenter.Instance.EventTrigger<Dialogue>("StartDialogue", currentDialogueGroup.dialogue_list[currentDialogueIndex]);
        }
    }


    public void ContinueDialogue()
    {
        if (currentDialogueGroup != null)
        {
            currentDialogueIndex++;
            if(currentDialogueIndex>=currentDialogueGroup.dialogue_list.Count)
            {
                EventCenter.Instance.EventTrigger<Dialogue>("EndDialogue", currentDialogueGroup.dialogue_list[currentDialogueIndex-1]);
                canContinue=false;
                currentDialogueGroup=null;
                currentDialogueIndex=0;
                return;
            }
            EventCenter.Instance.EventTrigger<Dialogue>("ContinueDialogue", currentDialogueGroup.dialogue_list[currentDialogueIndex]);
        }
        else
        {
            Debug.Log("当前并未有对话进行中，无法继续对话");
        }
    }

    public DialogueGroup GetDialogueGroupById(int id)
    {
        foreach(DialogueGroup dialogueGroup in dialogueGroup_list)
        {
            if(dialogueGroup.id == id)
                return dialogueGroup;
        }
        return null;
    }

    public void SetCanContinue(bool value)
    {
        canContinue = value;
    }
}
