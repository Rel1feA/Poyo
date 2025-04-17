using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    private bool isOpen;
    private BoxCollider2D boxCollider;
    public GameObject lockSprite;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        lockSprite.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isOpen)return;
        if(collision.CompareTag("Player"))
        {
            Player player=collision.GetComponent<Player>();
            if(player.hasKey)
            {
                isOpen = true;
                lockSprite.SetActive(false);
                boxCollider.enabled = false;
                player.hasKey = false;
            }
        }
    }
}
