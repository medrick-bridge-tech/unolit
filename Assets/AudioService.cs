using System.Collections;
using System.Collections.Generic;
using Medrick.Unolit.Service;
using Unity.VisualScripting;
using UnityEngine;

public class AudioService : Service
{
    //private AudioClip _audioClip;
    private AudioSourceFader _audioSourceFader1;
    private AudioSourceFader _audioSourceFader2;
    private AudioSourceFader _currentAudioSourceFader;
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

        if (currentAudioSourceFader)
        {
            return;
        }
        
        if (availableAudioSourceFader)
        {
            availableAudioSourceFader.FadeIn();
            availableAudioSourceFader.audioSource.clip = audioClip;
            availableAudioSourceFader.audioSource.Play();
        }
    }
    
    public void Pause()
    {
        //check if is already paused return;
        //otherwise pause it
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
            //currentAudioSourceFader.audioSource.Stop();
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
