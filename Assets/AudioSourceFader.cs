using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceFader : MonoBehaviour
{
    public AudioSource audioSource;
    private float _fadeDuration = 2f;
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

    public void SetCurrentClip(AudioClip clip)
    {
        currentClip = clip;
        audioSource.clip = clip;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInGradually());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutGradually());
    }
    

    private IEnumerator FadeInGradually()
    {
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
    }
}
