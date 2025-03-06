using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOperator
{
    void EnterOperator();
    void OnOperator();
    void ExitOperator();

    GameObject GetOperatorObject();
}
