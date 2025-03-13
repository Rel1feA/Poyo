using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorManager : Singleton<OperatorManager>
{
    public IOperator currentOperator;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

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
        EventCenter.Instance.EventTrigger<IOperator>("ChangeOperator",currentOperator);
    }
}
