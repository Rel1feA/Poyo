using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranferManager : Singleton<TranferManager>
{
    public LevelData levelDataSO;

    public RoomData currentRoomData;


    private void Start()
    {
        TranferRoom(1,1);
        DontDestroyOnLoad(gameObject);
    }

    public RoomData GetRoomData(int levelIndex, int roomID)
    {
        foreach(levelData levelData in levelDataSO.levelDates)
        {
            if(levelData.levelIndex == levelIndex)
            {
                foreach(RoomData roomData in levelData.roomData.roomDatas)
                {
                    if(roomData.roomID== roomID)
                    {
                        return roomData;
                    }
                }
            }
        }
        return null;
    }

    public void TranferRoom(int roomId)
    {
        RoomData roomData = GetRoomData(LevelManager.Instance.currentLevel.levelIndex,roomId);
        currentRoomData = roomData;
        EventCenter.Instance.EventTrigger<RoomData>("TransferRoom", roomData);
    }

    public void TranferRoom(int levelIndex, int roomId)
    {
        RoomData roomData = GetRoomData(levelIndex, roomId);
        currentRoomData = roomData;
        EventCenter.Instance.EventTrigger<RoomData>("TransferRoom", roomData);
    }
}
