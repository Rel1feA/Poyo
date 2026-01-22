using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Player : MonoBehaviour,ITakeDamge
{
    [HideInInspector]
    public PlayerController controller;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public bool hasKey;
    public PlayerData playerData;

    [SerializeField]
    private SpriteRenderer ghostDialogueSprite;


    public StateMachine stateMachine;
    public PlayerState idleState;
    public PlayerState operatorState;
    public PlayerState deadState;
    public PlayerState dialogueState;

    public bool hasDead { get; set; }

    private void OnEnable()
    {
        EventCenter.Instance.AddListener<CheckPointData>("RestartGame", OnRestartGame);
        EventCenter.Instance.AddListener("Reset", OnReset);
        EventCenter.Instance.AddListener<Dialogue>("StartDialogue", OnStartDialogue);
        EventCenter.Instance.AddListener<Dialogue>("EndDialogue", OnEndDialogue);
    }

    private void OnDisable()
    {
        EventCenter.Instance.RemoveListener<CheckPointData>("RestartGame", OnRestartGame);
        EventCenter.Instance.RemoveListener("Reset", OnReset);
        EventCenter.Instance.RemoveListener<Dialogue>("StartDialogue", OnStartDialogue);
        EventCenter.Instance.RemoveListener<Dialogue>("EndDialogue", OnEndDialogue);
    }

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        currentHealth = playerData.maxHealth;
        InitState();
        SetStartPos();
        TranferManager.Instance.TranferRoom(GameManager.Instance.checkPointData.roomID);
        hasKey = false;
        ghostDialogueSprite.enabled=false;
    }

    private void Update()
    {
        stateMachine.currentState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    private void LateUpdate()
    {
        stateMachine.CheckChangeState();
    }

    #region 监听重开游戏时的方法
    public void OnRestartGame(CheckPointData checkPointData)
    {
        Vector3 startPos= new Vector3(checkPointData.startPosX,checkPointData.startPosY,checkPointData.startPosZ);
        transform.position = startPos;
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
            ChangeState(PlayerStateType.Dead);
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
        deadState=new PlayerDeadState(stateMachine, this);
        dialogueState=new PlayerDialogue(stateMachine, this);
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
            case PlayerStateType.Dead:
                stateMachine.Changestate(deadState);
                break;
            case PlayerStateType.Dialogue:
                stateMachine.Changestate(dialogueState);
                break;
            default:
                break;
        }
    }
    #endregion

    #region 玩家死亡逻辑
    public void Dead()
    {
        EventCenter.Instance.EventTrigger("PlayerDead");
        controller.AniSetTrigger("Dead");
        controller.useRaduisObj.SetActive(false);
        OperatorManager.Instance.ChangeOperator(null);
        hasDead = true;
    }
    #endregion

    #region 播放死亡动画时执行的逻辑
    public void OnDeadAni()
    {
        GameManager.Instance.ReStart();
    }
    #endregion

    #region 初始化玩家位置逻辑
    public void SetStartPos()
    {
        Vector3 startPos=new Vector3(GameManager.Instance.checkPointData.startPosX,GameManager.Instance.checkPointData.startPosY, GameManager.Instance.checkPointData.startPosZ);
        transform.position = startPos;
    }
    #endregion

    #region 监听玩家重置游戏方法
    public void OnReset()
    {
        TakeDamge(9999);
    }
    #endregion

    #region 给动画机使用的调用声音的方法
    public void PlaySE(string seName)
    {
        AudioManager.Instance.PlayAudio(seName);
    }
    #endregion

    #region 展示对话灵魂
    public void ShowGhostDialogue()
    {
        ghostDialogueSprite.enabled = true;
        ghostDialogueSprite.DOColor(new Color(1, 1, 1, 1), 0.3f).From(new Color(1, 1, 1, 0));
    }
    #endregion

    #region 隐藏对话灵魂
    public void HideGhostDialogue()
    {
        ghostDialogueSprite.DOColor(new Color(1, 1, 1, 0), 0.3f).From(new Color(1, 1, 1, 1)).OnComplete(() =>
        {
            ghostDialogueSprite.enabled = false;
        });
    }
    #endregion

    #region 监听开始对话剧情的方法
    public void OnStartDialogue(Dialogue dialogue)
    {
        ChangeState(PlayerStateType.Dialogue);
    }
    #endregion

    #region 监听结束对话剧情的方法 
    public void OnEndDialogue(Dialogue dialogue)
    {
        ChangeState(PlayerStateType.Operator);
    } 
    #endregion
}
