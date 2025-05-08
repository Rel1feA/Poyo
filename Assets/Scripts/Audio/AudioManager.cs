using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public List<Sound> sounds;
    private Dictionary<string, AudioSource> audioDics=new Dictionary<string, AudioSource>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        foreach(Sound sound in sounds)
        {
            GameObject obj = new GameObject(sound.clip.name);
            obj.transform.SetParent(transform);
            AudioSource source= obj.AddComponent<AudioSource>();
            source.clip=sound.clip;
            source.playOnAwake = sound.playOnAwake;
            source.loop=sound.loop;
            source.volume=sound.volume;
            source.outputAudioMixerGroup=sound.mixerGroup;
            if(source.playOnAwake)
            {
                source.Play();
            }
            audioDics.Add(sound.clip.name, source);
        }
    }

    public void PlayAudio(string name)
    {
        if(!audioDics.ContainsKey(name))
        {
            return;
        }
        else
        {
            audioDics[name].Play();
        }
    }

    public void StopAudio()
    {
        if(!audioDics.ContainsKey(name))
        {
            return;
        }
        else
        {
            audioDics[name].Stop();
        }
    }
}

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    public AudioMixerGroup mixerGroup;

    [Range(0f, 1f)]
    public float volume=0.5f;

    public bool playOnAwake;

    public bool loop;
}

