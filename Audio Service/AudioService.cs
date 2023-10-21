using System.Collections;
using System.Collections.Generic;
using Medrick.Unolit.Service;
using UnityEngine;

public class AudioService : Service
{
    private AudioClip _audioClip;
    private AudioSource _audioSource;
    
    public void Initialize(AudioSource audioSource, AudioClip audioClip)
    {
        _audioSource = audioSource;
        _audioSource.clip = audioClip;
        _audioSource.loop = true;
    }
    
    public void Play()
    {
        _audioSource.Play();
    }
    
    public void Pause()
    {
        _audioSource.Pause();
    }
    
    public void Stop()
    {
        _audioSource.Stop();
    }
}
