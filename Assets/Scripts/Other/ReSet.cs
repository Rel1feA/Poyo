using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReSet : MonoBehaviour
{
    public int inRoomID;
    private bool hasBeTranfer;

    private void OnEnable()
    {
        EventCenter.Instance.AddListener<RoomData>("TranferRoom", CheckRetSet);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener<RoomData>("TranferRoom", CheckRetSet);
    }

    public void CheckRetSet(RoomData roomData)
    {
        if (TranferManager.Instance.currentRoomData.roomID == inRoomID) return;
        if (roomData.roomID != inRoomID)
        {
            if (hasBeTranfer) return;
            hasBeTranfer = true;
            Destroy(gameObject,0.5f);
            Instantiate(roomData.assetData);
        }
    }
}
