using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "Data/RoomData")]

public class RoomDataSO : ScriptableObject
{
    public List<RoomData> roomDatas;
}


[Serializable]
public class RoomData
{
    public int roomID;
    public int roomCameraID;
    public GameObject assetData;
}
