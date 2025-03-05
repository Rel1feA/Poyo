using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorManager : Singleton<OperatorManager>
{
    private IOperator currentOperator;

    private void Update()
    {
        if (currentOperator == null) return;
        currentOperator.OnOperator();
    }

    public void ChangeOperator(IOperator _operator)
    {
        if(_operator == null)
        {
            currentOperator = null;
            return;
        }
        if(currentOperator!=null)
        {
            currentOperator.ExitOperator();
        }
        currentOperator= _operator;
        currentOperator.EnterOperator();
    }
}
