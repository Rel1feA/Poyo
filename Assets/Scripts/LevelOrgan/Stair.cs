using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<IClimb>(out IClimb climb))
        {
            climb.CanClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IClimb>(out IClimb climb))
        {
            climb.CanClimb = false;
            climb.ExitClimb();
        }
    }
}
