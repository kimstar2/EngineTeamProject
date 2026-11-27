using System;
using _TevLib.ServiceLocatorSystem;
using _TevLib.ServiceLocatorSystem.AudioService;
using UnityEngine;
using AudioType = _TevLib.SoundSystem.Runtime.AudioType;

namespace Members.KJY.Scripts.Utils
{
    public class AudioFadeSetter : MonoBehaviour
    {
        [SerializeField] private float decibel;
        [SerializeField] private float duration;
        [SerializeField] private AudioType audioType;
        private IAudioService _audioService;

        private void Start()
        {
            _audioService = ServiceLocator.GetService<IAudioService>();
        }

        public void Set()
        {
            _audioService.SetFadeDecibel(audioType,decibel, duration);
        }
        
        public void Set(float db)
        {
            _audioService.SetFadeDecibel(audioType,db, duration);
        }
    }
}