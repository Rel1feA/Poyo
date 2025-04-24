using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="PlayerData",menuName ="Data/Player")]
public class PlayerData : ScriptableObject
{
    public float moveSpeed;
    public float jumpHeight;
    public float maxHealth;
    public float operatorCoolTime;
    public float normalGravityScale;
    public float fallGravityScale;
    public float climbSpeed;
    public Vector3 generateGhostPos;//使用技能时生成幽灵时与player的相对位置
}
