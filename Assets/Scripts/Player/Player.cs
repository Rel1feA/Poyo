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

    }

    public void OnRestartGame(CheckPoint checkPoint)
    {
        if(checkPoint==null)
        {
            transform.position=LevelManager.Instance.currentLevel.playerStartPos;
        }
        else
        {
            transform.position = checkPoint.transform.position;
        }
        controller.SetVel(Vector2.zero);
        OperatorManager.Instance.ChangeOperator(controller);
        currentHealth = playerData.maxHealth;
    }

    public void TakeDamge(float damage, UnityAction callback = null)
    {
        if (currentHealth <= 0) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Debug.Log("PlayerDead");
            EventCenter.Instance.EventTrigger("PlayerDead");
            OperatorManager.Instance.ChangeOperator(null);
            GameManager.Instance.ReStart();
        }
        callback?.Invoke();
    }
}
