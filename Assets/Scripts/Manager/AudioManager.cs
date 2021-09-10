using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    AudioSource audioSource;
    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    // Load AudioSource after loading scene
    public void SetAudioSourceClip(AudioClip clip)
    {
        audioSource.clip = clip;
        PlayAudioSource();
    }
    public void PlayAudioSource()
    {
        audioSource.Play();
    }
    public void PlayClipOneShot(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void StopAudio()
    {
        audioSource.Stop();
    }
}
