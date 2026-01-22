using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_DialogueObject
{
    Player,
    Ghost,
    Other
}

[CreateAssetMenu(fileName ="DialogueGroup",menuName ="Data/Dialogue")]
public class DialogueGroup : ScriptableObject
{
    public List<Dialogue> dialogue_list;
    public int id;
}

[Serializable]
public class Dialogue
{
    [TextArea]
    public string content;

    public E_DialogueObject dialogueObject;
}
