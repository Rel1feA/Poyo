using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class OpenVideo : MonoBehaviour
{
    private VideoPlayer player;
    private bool isSkip;

    private void Awake()
    {
        player = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        player.loopPointReached += OnVideoCompleted;
    }

    private void OnDestroy()
    {
        player.loopPointReached -= OnVideoCompleted;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&!isSkip)
        {
            OnVideoCompleted(player);
        }
    }

    public void OnVideoCompleted(VideoPlayer player)
    {
        isSkip = true;
        UIManager.Instance.ShowPanel<ShowTextPanel>("ShowTextPanel_1");
        player.loopPointReached -= OnVideoCompleted;
    }
}
