using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OpenVideo : MonoBehaviour
{
    private VideoPlayer player;

    private void Start()
    {
        player=GetComponent<VideoPlayer>();
        player.loopPointReached += OnVideoCompleted;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnVideoCompleted(null);
        }
    }

    public void OnVideoCompleted(VideoPlayer player)
    {
        LoadSceneManager.Instance.CreateTransferEffect(TransferEffectType.Fade, () =>
        {
            LevelManager.Instance.LoadLevel("Beach",null,(value) =>
            {
                LoadSceneManager.Instance.ChangeEffect();
                GameManager.Instance.ChangeState(GameStateType.OnGoing);
            });
        });
    }
}
