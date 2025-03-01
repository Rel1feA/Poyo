using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : Singleton<AudioManager>
{
    private AudioSource bgAudioSource=null;
    private GameObject seObj=null;
    private List<AudioSource> seSourcesList=new List<AudioSource>();
    public float BGValue=1;
    public float SeValue=1;


    private void Update()
    {
        for(int i=seSourcesList.Count-1; i>=0; i--)
        {
            if (!seSourcesList[i].isPlaying)
            {
                Destroy(seSourcesList[i]);
                seSourcesList.RemoveAt(i);
            }
        }
    }


    public void PlayBGMusic(string name)
    {
        if(bgAudioSource == null)
        {
            GameObject obj=new GameObject("BGSource");
            bgAudioSource = obj.AddComponent<AudioSource>();
        }

        ResourcesManager.Instance.LoadAsync<AudioClip>("Music/BG" + name,(clip)=>
        {
            bgAudioSource.clip = clip;
            bgAudioSource.loop = true;
            bgAudioSource.volume = BGValue;
            bgAudioSource.Play();
        });
    }

    public void PauseBGMusic()
    {
        if (bgAudioSource == null) return;
        bgAudioSource.Pause();
    }

    public void StopBGMusic()
    {
        if (bgAudioSource == null) return;
        bgAudioSource.Stop();
    }

    public void PlaySE(string name,bool isLoop=false, UnityAction callback=null)
    {
        if(seObj == null)
        {
            seObj = new GameObject("Sound");
        }
        ResourcesManager.Instance.LoadAsync<AudioClip>("Music/SE" + name, (clip) =>
        {
            AudioSource seSource= seObj.AddComponent<AudioSource>();
            seSource.clip = clip;
            seSource.volume = SeValue;
            seSource.loop = isLoop;
            seSource.Play();
            seSourcesList.Add(seSource);
            if(callback != null)
            {
                callback();
            }
        });
    }

    public void StopSE(AudioSource audioSource)
    {
        if(seSourcesList.Contains(audioSource))
        {
            seSourcesList.Remove(audioSource);
            audioSource.Stop();
            Destroy(seObj);
        }
    }

    public void ChangeSeValue(float value)
    {
        SeValue = value;
        for(int i = 0;i< seSourcesList.Count; i++)
        {
            seSourcesList[i].volume = value;
        }
    }
}
