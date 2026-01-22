using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    private int id;

    private bool hasTrigger;

    private void Start()
    {
        hasTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")&&!hasTrigger)
        {
            hasTrigger = true;
            DialogueManager.Instance.StartDialogue(id);
        }
    }
}
