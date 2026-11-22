    using _TevLib.ModuleSystem;
using _TevLib.ServiceLocatorSystem;
using _TevLib.ServiceLocatorSystem.AudioService;
using _TevLib.SoundSystem.Runtime;
using Members.KJY.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Members.KJY.Scripts.Player
{
    public class PlayerSound : MonoModule
    {
        [SerializeField] private SoundClipSO[] mouseClickSound;
        [SerializeField] private SoundClipSO[] anyKeySound;
        [SerializeField] private SoundClipSO[] keySpaceBarSound;
        [SerializeField] private SoundClipSO[] keyBackSpaceSound;
        [SerializeField] private SoundClipSO[] keyEnterSound;
        
        private IAudioService _audioService;

        private void Start()
        {
            _audioService = ServiceLocator.GetService<IAudioService>();
        }

        public void MouseClick()
        {
            _audioService.PlaySfx(mouseClickSound.AllRandom());
        }

        public void AnyKey()
        {
            _audioService.PlaySfx(anyKeySound.AllRandom());
        }   
        public void KeySpaceBar()
        {
            _audioService.PlaySfx(keySpaceBarSound.AllRandom());
        }

        public void KeyBackSpaceBar()
        {
            _audioService.PlaySfx(keyBackSpaceSound.AllRandom());
        }

        public void KeyEnter()
        {
            _audioService.PlaySfx(keyEnterSound.AllRandom());
        }
    }
}