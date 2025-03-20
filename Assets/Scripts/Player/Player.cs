using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public PlayerController controller;

    private void Start()
    {
        controller= GetComponent<PlayerController>();
    }
}
