using System;
using System.Collections;
using System.Collections.Generic;
using Medrick.Unolit.Service;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;

    private AudioService audioService;
    private AudioSource audioSource;
   
    public AudioClip[] AudioClips => audioClips;
    public AudioService AudioService => audioService;
    public AudioSource AudioSource => audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        ServiceLocator.Instance.Register<AudioService>(new AudioService());
        audioService = ServiceLocator.Instance.Locate<AudioService>();
        
        audioService.Initialize(audioSource);
    }
}
