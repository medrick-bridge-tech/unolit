using System;
using System.Collections;
using System.Collections.Generic;
using Medrick.Unolit.Service;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Medrick.Unolit.Service
{ 
    public class AudioService : Service
    {
        private List<AudioSourceFader> _audioSourceFaders = new List<AudioSourceFader>();

        public Action OnClipPaused;
        public Action OnClipPlayed;
        public Action OnClipStopped;

        public void Initialize(List<AudioSourceFader> audioSourceFaders)
        {
            for (int i = 0; i < audioSourceFaders.Count; i++)
            {
                _audioSourceFaders.Add(audioSourceFaders[i]);
                _audioSourceFaders[i].Loop = true;
            }
        }
    
        public void Play(AudioClip audioClip)
        {
            var currentAudioSourceFader = GetCurrentAudioSourceFader();

            if (currentAudioSourceFader && currentAudioSourceFader.audioSource.clip == audioClip)
            {
                OnClipPlayed?.Invoke();
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
                currentAudioSourceFader.audioSource.Pause();
            
            OnClipPaused?.Invoke();
        }
    
        public void UnPause()
        { 
            var currentAudioSourceFader = GetCurrentAudioSourceFader();
        
            if (currentAudioSourceFader)
                currentAudioSourceFader.audioSource.UnPause();
        }

        public void Stop()
        {
            var currentAudioSourceFader = GetCurrentAudioSourceFader();

            if (currentAudioSourceFader)
            {
                currentAudioSourceFader.FadeOut();
                OnClipStopped?.Invoke();
            }
        }

        private AudioSourceFader GetAvailableAudioSourceFader()
        {
            for (int i = 0; i < _audioSourceFaders.Count; i++)
            {
                if (_audioSourceFaders[i].audioSource.clip == null)
                    return _audioSourceFaders[i];
            }
            
            return null;
        }
    
        private AudioSourceFader GetCurrentAudioSourceFader()
        {
            for (int i = 0; i < _audioSourceFaders.Count; i++)
            {
                if (_audioSourceFaders[i].audioSource.clip != null)
                    return _audioSourceFaders[i];
            }
            
            return null;
        }
    }
}
