using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClimb
{
    public bool IsClimb {  get; set; }

    public bool CanClimb {  get; set; }

    public void EnterClimb();

    public void OnClimb();

    public void ExitClimb();
}
