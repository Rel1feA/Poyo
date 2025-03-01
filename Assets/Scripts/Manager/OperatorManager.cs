using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorManager : MonoBehaviour
{
    private IOperator currentOperator;

    private void Update()
    {
        if (currentOperator == null) return;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentOperator.Jump();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            currentOperator.UseSkill();
        }
        currentOperator.Move();
    }

    public void ChangeOperator(IOperator _operator)
    {
        currentOperator= _operator;
    }
}
