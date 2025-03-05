using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ITakeDamge
{
    void TakeDamge(float damage,UnityAction callback=null);
}
