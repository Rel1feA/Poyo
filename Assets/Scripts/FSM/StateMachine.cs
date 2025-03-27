using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State currentState;
    public State nextState;

    public void Init(State state)
    {
        currentState = state;
        currentState.EnterState();
    }

    public void Changestate(State newState)
    {
        nextState=newState;
    }


    /// <summary>
    /// 此方法用于改变状态时执行逻辑，必须放在LateUpdate里面执行，目的在于防止旧的状态的帧运行方法覆盖掉新状态的进入状态所执行的方法
    /// </summary>
    public void CheckChangeState()
    {
        if(nextState!= null)
        {
            currentState.ExitState();
            currentState = nextState;
            currentState.EnterState();
            nextState= null;
        }
    }
}
