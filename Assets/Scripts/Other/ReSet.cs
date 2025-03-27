using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReSet : MonoBehaviour
{
    public int inRoomID;
    public UnityEvent ResetAction;

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
        if (roomData.roomID != inRoomID) return;
        if (TranferManager.Instance.currentRoomData.roomID == inRoomID) return;
        ResetAction?.Invoke();
    }
}
