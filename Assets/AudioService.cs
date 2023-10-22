using System.Collections;
using System.Collections.Generic;
using Medrick.Unolit.Service;
using Unity.VisualScripting;
using UnityEngine;

public class AudioService : Service
{
    private AudioClip _audioClip;
    private AudioSource _audioSource;
    private float _fadeDuration = 2f;

    public void Initialize(AudioSource audioSource)
    {
        _audioSource = audioSource;
        _audioSource.loop = true;
        _audioSource.volume = 0f;
    }
    
    public IEnumerator Play(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();

        while (_audioSource.volume < 1f)
        {
            _audioSource.volume += Time.deltaTime / _fadeDuration;
            yield return null;
        }
    }
    
    public void Pause()
    {
        _audioSource.Pause();
    }

    public IEnumerator Stop()
    {
        while (_audioSource.volume > 0f)
        {
            _audioSource.volume -= Time.deltaTime / _fadeDuration;
            yield return null;
        }

        _audioSource.Stop();
    }
}
