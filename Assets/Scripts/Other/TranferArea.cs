using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranferArea : MonoBehaviour
{
    public int leftRoomID;
    public int rightRoomID;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(TranferManager.Instance.currentRoomData.roomID == leftRoomID)
            {
                TranferManager.Instance.TranferRoom(rightRoomID);
            }
            else if(TranferManager.Instance.currentRoomData.roomID == rightRoomID)
            {
                TranferManager.Instance.TranferRoom(leftRoomID);
            }
        }
    }
}
