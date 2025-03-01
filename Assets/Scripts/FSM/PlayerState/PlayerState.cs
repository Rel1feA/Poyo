using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : State
{
    private Player player;

    public PlayerState(StateMachine _stateMachine,Player player) : base(_stateMachine)
    {
        this.player = player;
    }
}