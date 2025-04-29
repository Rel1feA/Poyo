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
        DontDestroyOnLoad(gameObject);
        LoadSceneManager.Instance.LoadSceneAsync("StartGame");
        UIManager.Instance.ShowPanel<StartGamePlane>("StartGamePlane");
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            StartGame();
        }
    }

    #region 开始游戏的方法
    public void StartGame()
    {
        LoadSceneManager.Instance.CreateTransferEffect(TransferEffectType.Fade, () =>
        {
            LevelManager.Instance.LoadLevel("Beach", null, (value) =>
            {
                LoadSceneManager.Instance.ChangeEffect();
                UIManager.Instance.HidePanel("StartGamePlane");
            });
        });
    } 
    #endregion

    #region 玩家死亡时，重开游戏的方法
    public void ReStart()
    {
        if (checkPoint == null)
        {
            TranferManager.Instance.TranferRoom(1);
        }
        else
        {
            TranferManager.Instance.TranferRoom(checkPoint.inRoomID);
        }
        EventCenter.Instance.EventTrigger<CheckPoint>("RestartGame", checkPoint);
    }
    #endregion

    #region 监听关卡加载完成时所执行的方法
    public void OnCompletedLoadLevel(levelData levelData)
    {
        checkPoint = null;
    } 
    #endregion
}
