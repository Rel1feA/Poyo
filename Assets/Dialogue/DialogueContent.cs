using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DialogueContent
{
    [TextArea]
    public string[] contents;

    public UnityEvent startEvent;
    public UnityEvent endEvent;
}
