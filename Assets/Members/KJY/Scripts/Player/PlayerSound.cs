    using _TevLib.ModuleSystem;
using _TevLib.ServiceLocatorSystem;
using _TevLib.ServiceLocatorSystem.AudioService;
using _TevLib.SoundSystem.Runtime;
using Members.KJY.Scripts.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Members.KJY.Scripts.Player
{
    public class PlayerSound : MonoModule
    {
        [SerializeField] private SoundClipSO mouseClickSound;
        [SerializeField] private SoundClipSO[] keyEnterSound;
        [SerializeField] private SoundClipSO[] keyExitSound;
        private IAudioService _audioService;

        private void Start()
        {
            _audioService = ServiceLocator.GetService<IAudioService>();
        }

        public void MouseClick()
        {
            _audioService.PlaySfx(mouseClickSound);
        }
        
        public void KeyEnter()
        {
            _audioService.PlaySfx(keyEnterSound.AllRandom());
        }   
        public void KeyExit()
        {
            // _audioService.PlaySfx(keyExitSound.AllRandom());
        }
    }
}