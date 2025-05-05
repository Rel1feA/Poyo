using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneDoor : MonoBehaviour
{
    public string ToSceneName;
    public TransferEffectType effectType;
    public string TriggerTag;
    private bool hasChangeScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TriggerTag))
        {
            ChangeScene();
        }
    }

    public void ChangeScene()
    {
        if(hasChangeScene) return;
        LoadSceneManager.Instance.CreateTransferEffect(effectType, () =>
        {
            LoadSceneManager.Instance.LoadSceneAsync(ToSceneName,null,(value) =>
            {
                LoadSceneManager.Instance.ChangeEffect();
            });
        });
        hasChangeScene = true;
    }
}
