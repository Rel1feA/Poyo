using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class StoneTab : MonoBehaviour
{
    [SerializeField]
    private GameObject tipSprite;
    [SerializeField]
    private string textName;
    [SerializeField]
    private int dialogueID;

    private bool isPlayerInTrigger;
    private bool hasPlayDialogue;

    private void Start()
    {
        tipSprite.SetActive(false);
        isPlayerInTrigger = false;
        hasPlayDialogue = false;
    }

    private void Update()
    {
        if (isPlayerInTrigger&&Input.GetKeyDown(KeyCode.W))
        {
            if(UIManager.Instance.GetPanel<StoneTextPanel>(textName)==null)
            {
                UIManager.Instance.ShowPanel<StoneTextPanel>(textName);
                AudioManager.Instance.PlayAudio("StoneTab");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isPlayerInTrigger=true;
            tipSprite.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            tipSprite.SetActive(false);
            if(UIManager.Instance.GetPanel<StoneTextPanel>(textName)!=null)
            {
                UIManager.Instance.HidePanel(textName);
                AudioManager.Instance.PlayAudio("StoneTab");
                if(!hasPlayDialogue&&dialogueID!=0)
                {
                    DialogueManager.Instance.StartDialogue(dialogueID);
                    hasPlayDialogue=true;
                }
            }

        }
    }
}
