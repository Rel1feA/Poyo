using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData",menuName ="Data/levelData")]
public class LevelData : ScriptableObject
{
    public List<levelData> levelDates; 
}


[Serializable]
public class levelData
{
    public string levelName;
    public string levelSceneName;
    public int levelIndex;
    public RoomDataSO roomData;
    public Vector3 playerStartPos;
}

