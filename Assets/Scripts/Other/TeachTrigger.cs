using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachTrigger : MonoBehaviour
{
    public string planeName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            UIManager.Instance.ShowPanel<TeachUIPlane>("TeachPlane/"+planeName);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UIManager.Instance.HidePanel("TeachPlane/" + planeName);
        }
    }
}
