using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public LevelData LevelDateSO;

    public levelData currentLevel;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public levelData GetLevelData(string levelName)
    {
        foreach(levelData levelDate in LevelDateSO.levelDates)
        {
            if(levelDate.levelName == levelName)
            {
                return levelDate;
            }
        }
        return null;
    }

    public levelData GetLevelData(int levelIndex)
    {
        foreach (levelData levelDate in LevelDateSO.levelDates)
        {
            if (levelDate.levelIndex == levelIndex)
            {
                return levelDate;
            }
        }
        return null;
    }

    public void LoadLevel(string levelName,UnityAction<float> OnLoading=null,UnityAction<AsyncOperation> onCompleted = null,bool sceneActive=true)
    {
        levelData targetLevel = GetLevelData(levelName);
        LoadSceneManager.Instance.LoadSceneAsync(targetLevel.levelSceneName, OnLoading, (value) =>
        {
            onCompleted?.Invoke(value);
            AudioManager.Instance.PlayAudio(targetLevel.BGMName);
            EventCenter.Instance.EventTrigger<levelData>("CompletedLoadLevel", targetLevel);
        },sceneActive);
        currentLevel = targetLevel;
    }

    public void LoadLevel(int levelIndex,UnityAction<float> OnLoading=null,UnityAction<AsyncOperation> onCompleted=null, bool sceneActive = true)
    {
        levelData targetLevel = GetLevelData(levelIndex);
        LoadSceneManager.Instance.LoadSceneAsync(targetLevel.levelSceneName, OnLoading, (value) =>
        {
            onCompleted?.Invoke(value);
            AudioManager.Instance.PlayAudio(targetLevel.BGMName);
            EventCenter.Instance.EventTrigger<levelData>("CompletedLoadLevel", targetLevel);
        },sceneActive);
        currentLevel = targetLevel;
    }

    public void ReLoadLevel()
    {
        LoadLevel(currentLevel.levelName);
    }
}
