using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelDoor : MonoBehaviour
{
    public TransferEffectType effectType;
    public int toLevelIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            LoadSceneManager.Instance.CreateTransferEffect(effectType,() =>
            {
                LevelManager.Instance.LoadLevel(toLevelIndex, null, (value) =>
                {
                    LoadSceneManager.Instance.ChangeEffect();
                });
            });
        }
    }
}
