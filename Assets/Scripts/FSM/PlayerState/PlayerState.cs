using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    private PlayerController player;

    public PlayerState(StateMachine _stateMachine,PlayerController player) : base(_stateMachine)
    {
        this.player = player;
    }
}