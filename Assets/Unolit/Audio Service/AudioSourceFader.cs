using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceFader : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private float _fadeDuration = 2f;
    private AudioClip currentClip;

    public AudioClip CurrentClip => currentClip;

    public bool IsPlaying => audioSource.isPlaying;
    public bool Loop
    {
        get => audioSource.loop;
        set => audioSource.loop = value;
    }
    
    public float Volume
    {
        get => audioSource.volume;
        set => audioSource.volume = value;
    }

    private Coroutine _fadeInCoroutine;
    private Coroutine _fadeOutCoroutine;

    public RoomManager roomManager;

    private void OnEnable()
    {
        roomManager.AudioService.OnClipPaused += StopFadeInCoroutine;
        roomManager.AudioService.OnClipPlayed += StopFadeOutCoroutine;
        roomManager.AudioService.OnClipStopped += StopFadeInCoroutine;
    }

    private void OnDisable()
    {
        roomManager.AudioService.OnClipPaused -= StopFadeInCoroutine;
        roomManager.AudioService.OnClipPlayed -= StopFadeOutCoroutine;
        roomManager.AudioService.OnClipStopped -= StopFadeInCoroutine;
    }

    public void SetCurrentClip(AudioClip clip)
    {
        currentClip = clip;
        audioSource.clip = clip;
    }

    public void FadeIn(float volume)
    {
        _fadeInCoroutine = StartCoroutine(FadeInGradually(volume));
    }

    public void FadeOut()
    {
        _fadeOutCoroutine = StartCoroutine(FadeOutGradually());
    }
    
    private IEnumerator FadeInGradually(float startingVolume)
    {
        audioSource.volume = startingVolume;
        
        while (audioSource.volume < 1f)
        {
            audioSource.volume += Time.deltaTime / _fadeDuration;
            yield return null;
        }
    }

    private IEnumerator FadeOutGradually()
    {
        while (audioSource.volume > 0f)
        {
            audioSource.volume -= Time.deltaTime / _fadeDuration;
            yield return null;
        }
        
        audioSource.Stop();
        audioSource.clip = null;
    }
    
    public void StopFadeInCoroutine()
    {
        StopCoroutine(_fadeInCoroutine);
    }
    
    public void StopFadeOutCoroutine()
    {
        StopCoroutine(_fadeOutCoroutine);
    }
}
