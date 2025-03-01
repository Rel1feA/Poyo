using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{
    private bool isCheck=false;

    private void Update()
    {
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.D);
        CheckKeyCode(KeyCode.A);
    }

    public void IsCheckInput(bool _isCheck)
    {
        isCheck = _isCheck;
    }

    private void CheckKeyCode(KeyCode keyCode)
    {
        if (!isCheck) return;
        if (Input.GetKeyDown(keyCode))
        {
            EventCenter.Instance.EventTrigger("KeyDown", keyCode);
        }
        if (Input.GetKeyUp(keyCode))
        {
            EventCenter.Instance.EventTrigger("KeyUp", keyCode);
        }
    }
}
