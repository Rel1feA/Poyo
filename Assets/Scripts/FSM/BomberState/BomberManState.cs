using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberManState : State
{
    public BomberMan bomberMan;
    public BomberManState(StateMachine _stateMachine,BomberMan bomberMan) : base(_stateMachine)
    {
        this.bomberMan = bomberMan;
    }
}
