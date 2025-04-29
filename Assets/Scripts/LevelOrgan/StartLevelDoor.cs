using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelDoor : MonoBehaviour
{
    public TransferEffectType effectType;

    private void Awake()
    {
        LoadSceneManager.Instance.CreateTransferEffect(effectType);
    }
}
