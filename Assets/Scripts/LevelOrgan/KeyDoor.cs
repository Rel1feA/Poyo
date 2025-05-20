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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isOpen) return;
        if (collision.transform.CompareTag("Player"))
        {
            Player player = collision.transform.GetComponent<Player>();
            if (player.hasKey)
            {
                isOpen = true;
                lockSprite.SetActive(false);
                boxCollider.enabled = false;
                player.hasKey = false;
            }
        }
    }
}
