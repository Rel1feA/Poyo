using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOperator
{
    bool canOperator { get; set; }

    void EnterOperator();
    void OnOperator();
    void ExitOperator();

    GameObject GetOperatorObject();
}
