using System.Collections;
using System.Collections.Generic;
using Medrick.Unolit.Service;
using Unity.VisualScripting;
using UnityEngine;

public class AudioService : Service
{
    private AudioSourceFader _audioSourceFader1;
    private AudioSourceFader _audioSourceFader2;
    private float _fadeDuration = 2f;

    public void Initialize(AudioSourceFader audioSourceFader1, AudioSourceFader audioSourceFader2)
    {
        _audioSourceFader1 = audioSourceFader1;
        _audioSourceFader2 = audioSourceFader2;
        _audioSourceFader1.Loop = true;
        _audioSourceFader2.Loop = true;
        _audioSourceFader1.Volume = 0f;
        _audioSourceFader2.Volume = 0f;
    }
    
    public void Play(AudioClip audioClip)
    {
        var availableAudioSourceFader = GetAvailableAudioSourceFader();
        var currentAudioSourceFader = GetCurrentAudioSourceFader();

        if (availableAudioSourceFader)
        {
            availableAudioSourceFader.audioSource.clip = audioClip;
            availableAudioSourceFader.audioSource.Play();
            availableAudioSourceFader.FadeIn();
            currentAudioSourceFader.audioSource.clip = null;
        }
    }
    
    public void Pause()
    {
        var availableAudioSourceFader = GetAvailableAudioSourceFader();
        var currentAudioSourceFader = GetCurrentAudioSourceFader();
        
        if (currentAudioSourceFader)
        {
            currentAudioSourceFader.audioSource.Pause();
        }
        
        currentAudioSourceFader.StopFadeInCoroutine();
    }
    
    public void UnPause()
    {
        var availableAudioSourceFader = GetAvailableAudioSourceFader();
        var currentAudioSourceFader = GetCurrentAudioSourceFader();
        
        if (currentAudioSourceFader)
        {
            currentAudioSourceFader.audioSource.UnPause();
        }
    }

    public void Stop()
    {
        var availableAudioSourceFader = GetAvailableAudioSourceFader();
        var currentAudioSourceFader = GetCurrentAudioSourceFader();
        
        if (availableAudioSourceFader)
        {
            availableAudioSourceFader.audioSource.Stop();
        }

        if (currentAudioSourceFader)
        {
            currentAudioSourceFader.FadeOut();
        }
    }

    private AudioSourceFader GetAvailableAudioSourceFader()
    {
        if (_audioSourceFader1.audioSource.clip == null)
        {
            return _audioSourceFader1;
        }
        else if(_audioSourceFader2.audioSource.clip == null)
        {
            return _audioSourceFader2;
        }
        else
        {
            return null;
        }
    }
    
    private AudioSourceFader GetCurrentAudioSourceFader()
    {
        if (_audioSourceFader1.audioSource.clip != null)
        {
            return _audioSourceFader1;
        }
        else if(_audioSourceFader2.audioSource.clip != null)
        {
            return _audioSourceFader2;
        }
        else
        {
            return null;
        }
    }
}
