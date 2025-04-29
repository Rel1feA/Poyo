using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGamePlane : BasePanel
{
    private void Start()
    {
        GetControl<Button>("StartGameBTN").onClick.AddListener(GameManager.Instance.StartGame);
    }
}
