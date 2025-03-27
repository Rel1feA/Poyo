using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public PlayerController controller;

    public bool hasKey;

    private void Start()
    {
        controller= GetComponent<PlayerController>();

    }
}
