using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OpenVideo : MonoBehaviour
{
    public VideoPlayer player;

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
            LoadSceneManager.Instance.LoadSceneAsync("StartGame",null,(value) =>
            {
                LoadSceneManager.Instance.ChangeEffect();
                Destroy(gameObject);
            });
        });
    }
}
