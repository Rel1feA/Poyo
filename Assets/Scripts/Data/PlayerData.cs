using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="PlayerData",menuName ="Data/Player")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed;
    public float jumpHeight;
}
