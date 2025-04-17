using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour,ITakeDamge
{
    [HideInInspector]
    public PlayerController controller;
    private float currentHealth;
    private float health;
    public PlayerData playerData;
    public bool hasKey;

    public StateMachine stateMachine;
    public PlayerState idleState;
    public PlayerState operatorState;

    public bool hasDead { get; set; }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener<CheckPoint>("RestartGame", OnRestartGame);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener<CheckPoint>("RestartGame", OnRestartGame);
    }

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        currentHealth = playerData.maxHealth;
        if (GameManager.Instance.checkPoint == null)
        {
            transform.position = LevelManager.Instance.currentLevel.playerStartPos;
        }
        else
        {
            transform.position = GameManager.Instance.checkPoint.transform.position;
        }
        InitState();
    }

    private void Update()
    {
        stateMachine.currentState.FrameUpdate();
    }

    private void LateUpdate()
    {
        stateMachine.CheckChangeState();
    }

    #region 监听重开游戏时的方法
    public void OnRestartGame(CheckPoint checkPoint)
    {
        if (checkPoint == null)
        {
            transform.position = LevelManager.Instance.currentLevel.playerStartPos;
        }
        else
        {
            transform.position = checkPoint.transform.position;
        }
        controller.SetVel(Vector2.zero);
        controller.isOnWake= true;
        OperatorManager.Instance.ChangeOperator(controller);
        currentHealth = playerData.maxHealth;
        hasDead = false;
    }
    #endregion

    #region 实现Idamage接口的方法
    public void TakeDamge(float damage, UnityAction callback = null)
    {
        if (hasDead) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            EventCenter.Instance.EventTrigger("PlayerDead");
            controller.useRaduisObj.SetActive(false);
            OperatorManager.Instance.ChangeOperator(null);
            hasDead = true;
            GameManager.Instance.ReStart();
        }
        callback?.Invoke();
    }
    #endregion

    #region 初始化状态代码
    public void InitState()
    {
        stateMachine = new StateMachine();
        idleState = new PlayerIdle(stateMachine, this);
        operatorState=new PlayerOperator(stateMachine, this);
        stateMachine.Init(operatorState);
    }
    #endregion

    #region 给外部调用的转换状态方法
    public void ChangeState(PlayerStateType state)
    {
        switch(state)
        {
            case PlayerStateType.Idle:
                stateMachine.Changestate(idleState);
                break;
            case PlayerStateType.Operator:
                stateMachine.Changestate(operatorState);
                break;
            default:
                break;
        }
    } 
    #endregion

}
