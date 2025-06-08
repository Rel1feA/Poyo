using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : Singleton<GameManager>
{
    public CheckPointData checkPointData;

    public StateMachine stateMachine;
    public State pauseState;
    public State onGoingState;
    public State normalState;

    public GameStateType currentState;

    private bool isFirstPlay;

    private void Update()
    {
        stateMachine.currentState.FrameUpdate();
    }

    private void LateUpdate()
    {
        stateMachine.CheckChangeState();
    }

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
        LoadSaveDataToInitGame();
        LoadSceneManager.Instance.LoadSceneAsync("StartGame");
        AudioManager.Instance.PlayAudio("MainMenu");
        InitState();
    }

    #region 读取用户存档初始化游戏方法
    public void LoadSaveDataToInitGame()
    {
        UserData userData= SaveManager.Instance.GetUserData("A");
        AudioManager.Instance.mixer.SetFloat("BGM",userData.bgmValue);
        AudioManager.Instance.mixer.SetFloat("SE", userData.seValue);
        Screen.fullScreen = userData.isFullScreen;
        ChangeResolution(userData.resolutionWidth);
        isFirstPlay=userData.isFirstOpenGame;
        checkPointData = userData.checkPointData;
    } 
    #endregion

    #region 开始游戏的方法
    public void StartGame()
    {
        if(isFirstPlay)
        {
            LoadSceneManager.Instance.CreateTransferEffect(TransferEffectType.Fade, () =>
            {
                LoadSceneManager.Instance.LoadSceneAsync("OpenScene", null, (value) =>
                {
                    LoadSceneManager.Instance.ChangeEffect();
                });
            });
            isFirstPlay=false;
            return;
        }
        LoadSceneManager.Instance.CreateTransferEffect(TransferEffectType.Fade, () =>
        {
            if(checkPointData != null)
            {
                LevelManager.Instance.LoadLevel(checkPointData.levelName, null, (value) =>
                {
                    LoadSceneManager.Instance.ChangeEffect();
                    ChangeState(GameStateType.OnGoing);
                });
            }
        });
    }
    #endregion

    #region 重新开始游戏的方法
    public void ReStart()
    {
        TranferManager.Instance.TranferRoom(checkPointData.roomID);
        EventCenter.Instance.EventTrigger<CheckPointData>("RestartGame", checkPointData);
    }
    #endregion

    #region 监听关卡完成加载时的方法
    public void OnCompletedLoadLevel(levelData levelData)
    {
        if(checkPointData.levelName.Equals(levelData.levelName))
        {
            return;
        }
        checkPointData.levelName=levelData.levelName;
        checkPointData.roomID = 1;
        checkPointData.startPosX=levelData.playerStartPos.x;
        checkPointData.startPosY = levelData.playerStartPos.y;
        checkPointData.startPosZ = levelData.playerStartPos.z;
    }
    #endregion

    #region 暂停游戏的方法
    public void PauseGame()
    {
        Time.timeScale = 0;
        ChangeState(GameStateType.Pause);
        UIManager.Instance.ShowPanel<PauseUIPlane>("PauseUIPlane",E_UI_Layer.System);
    }
    #endregion

    #region 继续游戏的方法
    public void ResumeGame()
    {
        Time.timeScale = 1;
        ChangeState(GameStateType.OnGoing);
        UIManager.Instance.HidePanel("PauseUIPlane");
    }
    #endregion

    #region 返回主菜单的方法
    public void BackToMenu()
    {
        ChangeState(GameStateType.Normal);
        UpdateUserData();
        LoadSceneManager.Instance.CreateTransferEffect(TransferEffectType.Fade, () =>
        {
            LoadSceneManager.Instance.LoadSceneAsync("StartGame", null, (value) =>
            {
                AudioManager.Instance.PlayAudio("MainMenu");
                LoadSceneManager.Instance.ChangeEffect();
                Time.timeScale = 1;
            });
        });
        UIManager.Instance.HidePanel("PauseUIPlane");
    } 
    #endregion

    #region 退出游戏的方法
    public void QuitGame()
    {
        UpdateUserData();
        Application.Quit();
    }
    #endregion

    #region 初始化状态的代码
    public void InitState()
    {
        stateMachine = new StateMachine();
        pauseState = new PauseState(stateMachine);
        onGoingState=new OnGoingState(stateMachine);
        normalState= new NormalState(stateMachine);
        stateMachine.Init(normalState);
        currentState=GameStateType.Normal;
    }
    #endregion

    #region 给外部调用的改变状态的方法
    public void ChangeState(GameStateType type)
    {
        switch (type)
        {
            case GameStateType.Normal:
                stateMachine.Changestate(normalState);
                currentState = GameStateType.Normal;
                break;
            case GameStateType.Pause:
                stateMachine.Changestate(pauseState);
                currentState = GameStateType.Pause;
                break;
            case GameStateType.OnGoing:
                stateMachine.Changestate(onGoingState);
                currentState = GameStateType.OnGoing;
                break;
            default:
                break;
        }
    }
    #endregion

    #region 切换是否全屏模式的方法
    public void ChangeFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    #endregion

    #region 切换分辨率方法
    public void ChangeResolution(int value)
    {
        switch(value)
        {
            case 0:
                Screen.SetResolution(1920, 1080,Screen.fullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case 2:
                Screen.SetResolution(1024,576,Screen.fullScreen);
                break;
            default:
                break;
        }
    }

    public void ChangeResolution(float width)
    {
        switch (width)
        {
            case 1920:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 1280:
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
            case 1024:
                Screen.SetResolution(1024, 576, Screen.fullScreen);
                break;
            default:
                break;
        }
    }
    #endregion

    #region 更新目前游戏的设置到用户存档中
    public void UpdateUserData()
    {
        float bgmValue;
        AudioManager.Instance.mixer.GetFloat("BGM",out bgmValue);
        float seValue;
        AudioManager.Instance.mixer.GetFloat("SE", out seValue);
        float resolutionWidth=Screen.width;
        bool _isFirstPlay = isFirstPlay;
        bool isFullScreen=Screen.fullScreen;
        UserData userData=new UserData("A",bgmValue,seValue,resolutionWidth,isFullScreen,_isFirstPlay,checkPointData);
        SaveManager.Instance.SaveUserData(userData);
    } 
    #endregion
}
