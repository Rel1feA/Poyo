using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void Start()
    {
        LevelManager.Instance.LoadLevel("level2");
        DontDestroyOnLoad(gameObject);
    }

    public void ReStart()
    {
        LevelManager.Instance.ReLoadLevel();
    }
}
