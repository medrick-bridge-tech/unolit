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
    }
    
    public void Play(AudioClip audioClip)
    {
        var currentAudioSourceFader = GetCurrentAudioSourceFader();

        if (currentAudioSourceFader && currentAudioSourceFader.audioSource.clip == audioClip)
        {
            currentAudioSourceFader.StopFadeOutCoroutine();
            currentAudioSourceFader.FadeIn(currentAudioSourceFader.Volume); 
        }
        else
        {
            var availableAudioSourceFader = GetAvailableAudioSourceFader();

            if (availableAudioSourceFader)
            {
                availableAudioSourceFader.audioSource.clip = audioClip;
                availableAudioSourceFader.audioSource.Play();
                availableAudioSourceFader.FadeIn(0);
            }
        }
        
    }
    
    public void Pause()
    {
        var currentAudioSourceFader = GetCurrentAudioSourceFader();
        
        if (currentAudioSourceFader)
        {
            currentAudioSourceFader.audioSource.Pause();
        }
        
        currentAudioSourceFader.StopFadeInCoroutine();
    }
    
    public void UnPause()
    {
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

        if (currentAudioSourceFader)
        {
            currentAudioSourceFader.FadeOut();
            currentAudioSourceFader.StopFadeInCoroutine();
        }
    }

    private AudioSourceFader GetAvailableAudioSourceFader()
    {
        if (_audioSourceFader1.audioSource.clip == null)
            return _audioSourceFader1;
        else if(_audioSourceFader2.audioSource.clip == null)
            return _audioSourceFader2;
        else
            return null;
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
