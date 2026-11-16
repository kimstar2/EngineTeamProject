using System;
using _TevLib.ModuleSystem;
using _TevLib.ServiceLocatorSystem;
using _TevLib.ServiceLocatorSystem.AudioService;
using _TevLib.SoundSystem.Runtime;
using Members.KJY.Scripts.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace Members.KJY.Scripts.Player
{
    public class PlayerController : ModuleOwner
    {
        [SerializeField] private PlayerInputSO playerInputData;

        public PlayerSound PlayerSound { get; private set; }
        
        
        protected override void InitializeModules()
        {
            base.InitializeModules();
            PlayerSound = GetModule<PlayerSound>();
        }

        private void Update()
        {
            bool on = false;
            if (Keyboard.current.backspaceKey.wasPressedThisFrame)
            {
                PlayerSound.KeyBackSpaceBar();
                on = true;
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                PlayerSound.KeySpaceBar();
                on = true;
            }
            
            if (on)
                return;
            
            if (Keyboard.current.anyKey.wasPressedThisFrame)
                PlayerSound.KeyEnter();
        }

        private void OnEnable()
        {
            playerInputData.SetEnable();
            
            playerInputData.OnAttackKeyPressed += PlayerSound.MouseClick;
        }

        private void OnDisable()
        {
            playerInputData.OnAttackKeyPressed -= PlayerSound.MouseClick;
            
            playerInputData.SetDisable();
        }
    }
}
