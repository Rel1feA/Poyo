using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranferManager : Singleton<TranferManager>
{
    public RoomDataSO roomDataSO;

    public RoomData currentRoomData;

    private void Start()
    {
        TranferRoom(1);
        DontDestroyOnLoad(gameObject);
    }

    public RoomData GetRoomData(int roomId)
    {
        foreach(RoomData roomData in roomDataSO.roomDatas)
        {
            if(roomData.roomID == roomId)
            {
                return roomData;
            }
        }
        return null;
    }

    public void TranferRoom(int roomId)
    {
        RoomData roomData = GetRoomData(roomId);
        currentRoomData = roomData;
        EventCenter.Instance.EventTrigger<RoomData>("TranferRoom", roomData);
    }
}
