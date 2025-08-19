using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dialoguemanager : Singleton<Dialoguemanager>
{
    private DialogueContent currentContent;
    private Queue<string> contents;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        contents = new Queue<string>();
    }

    public void StartDialogue(DialogueContent content)
    {
        currentContent = content;
        currentContent.startEvent?.Invoke();
        contents.Clear();
        foreach(string _content in currentContent.contents)
        {
            contents.Enqueue(_content);
        }
        UIManager.Instance.ShowPanel<DialogueUIPlane>("DialogueUIPlane");
    }

    public string DisplayDialogue()
    {
        if (contents.Count==0)
        {
            UIManager.Instance.HidePanel("DialogueUIPlane");
            currentContent.endEvent?.Invoke();
            return string.Empty;
        }
        return contents.Dequeue();
    }

}
