using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int inRoomID;
    public string LevelName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlayAudio("CheckPoint");
            GameManager.Instance.checkPointData.startPosX = transform.position.x;
            GameManager.Instance.checkPointData.startPosY = transform.position.y;
            GameManager.Instance.checkPointData.startPosZ = transform.position.z;
            GameManager.Instance.checkPointData.levelName = LevelName;
            GameManager.Instance.checkPointData.roomID = inRoomID;
        }
    }
}
