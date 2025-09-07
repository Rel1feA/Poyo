using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;


public class SaveManager : NormalSingleton<SaveManager>
{
    public Dictionary<string,UserData> datas = new Dictionary<string,UserData>();

    public void SaveUserData(UserData userData)
    {
        if (!File.Exists(Application.persistentDataPath + "/users"))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/users");
        }
        datas[userData.Name] = userData;
        string jsonData=JsonConvert.SerializeObject(userData);
        File.WriteAllText(Application.persistentDataPath + string.Format("/users/{0}.json", userData.Name), jsonData);
    }

    public UserData GetUserData(string userName)
    {
        if(datas.ContainsKey(userName))
        {
            return datas[userName];
        }
        string path=Application.persistentDataPath+ string.Format("/users/{0}.json", userName);
        if(File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            UserData userData = JsonConvert.DeserializeObject<UserData>(jsonData);
            return userData;
        }
        else
        {
            CheckPointData checkPointData = new CheckPointData();
            checkPointData.roomID = 1;
            checkPointData.levelName = "Beach";
            Vector3 startPos= LevelManager.Instance.GetLevelData("Beach").playerStartPos;
            checkPointData.startPosX = startPos.x;
            checkPointData.startPosY = startPos.y;
            checkPointData.startPosZ = startPos.z;
            UserData userData=new UserData("A",-30,-30,1920,true,true,checkPointData);
            SaveUserData(userData);
            Debug.Log(userData.checkPointData.levelName);
            return userData;
        }
    }
}

public class UserData
{
    public string Name;
    public float bgmValue;
    public float seValue;
    public float resolutionWidth;
    public bool isFullScreen;
    public bool isFirstOpenGame;
    public CheckPointData checkPointData;

    public UserData(string name, float BGMValue, float SEValue,float ResolutionWidth,bool isFullScreen, bool isFirstOpenGame,CheckPointData checkPointData)
    {
        Name = name;
        bgmValue = BGMValue;
        seValue = SEValue;
        resolutionWidth = ResolutionWidth;
        this.isFullScreen = isFullScreen;
        this.isFirstOpenGame = isFirstOpenGame;
        this.checkPointData = checkPointData;
    }
}

public class CheckPointData
{
    public float startPosX;
    public float startPosY;
    public float startPosZ;
    public int roomID;
    public int maxFinishLevel;
    public string levelName;
}


