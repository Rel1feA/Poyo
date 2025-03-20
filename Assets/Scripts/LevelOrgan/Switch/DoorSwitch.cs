using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch :MonoBehaviour,ISwitch
{
    public AutoDoor autoDoor;
    public Vector2 forceDir;

    public void CanTrigger(IOperator _operator)
    {
        autoDoor.AddForceToDoor(forceDir);
    }
}
