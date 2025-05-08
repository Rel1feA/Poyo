using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CheckPoint checkPoint;

    public StateMachine stateMachine;
    public State pauseState;
    public State onGoingState;
    public State normalState;

    public GameStateType currentState;

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
        LoadSceneManager.Instance.LoadSceneAsync("StartGame");
        InitState();
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
                ChangeState(GameStateType.OnGoing);
            });
        });
    } 
    #endregion

    #region 重新开始游戏的方法
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

    #region 监听下一关关卡完成加载时的方法
    public void OnCompletedLoadLevel(levelData levelData)
    {
        checkPoint = null;
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
        LoadSceneManager.Instance.CreateTransferEffect(TransferEffectType.Fade, () =>
        {
            LoadSceneManager.Instance.LoadSceneAsync("StartGame", null, (value) =>
            {
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
}
