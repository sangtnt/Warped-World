using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Init things when scene was loaded
public class LoadSceneConfig : MonoBehaviour
{
    [SerializeField]
    AudioClip backgroundMusic;
    public virtual void Start()
    {
        InitBackgroundMusic();
    }
    public virtual void InitBackgroundMusic()
    {
        AudioManager.Instance.SetAudioSourceClip(backgroundMusic);
    }
}
