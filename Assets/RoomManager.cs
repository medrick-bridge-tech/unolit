using System;
using System.Collections;
using System.Collections.Generic;
using Medrick.Unolit.Service;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioSourceFader audioSourceFader1;
    [SerializeField] private AudioSourceFader audioSourceFader2;

    private AudioService audioService;

    public AudioClip[] AudioClips => audioClips;
    public AudioService AudioService => audioService;

    void Awake()
    {
        ServiceLocator.Instance.Register<AudioService>(new AudioService());
        audioService = ServiceLocator.Instance.Locate<AudioService>();
        
        audioService.Initialize(audioSourceFader1, audioSourceFader2);
        
        audioService.Play(audioClips[0]);
        audioService.Play(audioClips[1]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioService.Stop();
        }
    }
}
