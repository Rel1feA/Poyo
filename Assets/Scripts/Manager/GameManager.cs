using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CheckPoint checkPoint;

    private void OnEnable()
    {
        EventCenter.Instance.AddListener<levelData>("CompletedLoadLevel", OnCompletedLoadLevel);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener<levelData>("CompletedLoadLevel", OnCompletedLoadLevel);
    }

    private void Start()
    {
        LevelManager.Instance.LoadLevel("level2");
        DontDestroyOnLoad(gameObject);
    }

    public void ReStart()
    {
        if(checkPoint==null)
        {
            TranferManager.Instance.TranferRoom(1);
        }
        else
        {
            TranferManager.Instance.TranferRoom(checkPoint.inRoomID);
        }
        EventCenter.Instance.EventTrigger<CheckPoint>("RestartGame", checkPoint);
    }

    public void OnCompletedLoadLevel(levelData levelData)
    {
        checkPoint = null;
    }
}
