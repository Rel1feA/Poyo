using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateType
{
    Pause,
    OnGoing,
    Normal
}

public class PauseState : State
{
    public PauseState(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void FrameUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.ResumeGame();
        }
    }
}

public class OnGoingState : State
{
    public OnGoingState(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void FrameUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PauseGame();
        }
    }
}

public class NormalState : State
{
    public NormalState(StateMachine stateMachine) : base(stateMachine) 
    {

    }
}



