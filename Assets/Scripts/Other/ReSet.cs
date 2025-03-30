using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReSet : MonoBehaviour
{
    public int inRoomID;

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
            Destroy(gameObject,0.5f);
            Instantiate(roomData.assetData);
        }
    }
}
