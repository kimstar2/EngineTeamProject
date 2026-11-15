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

        private void OnEnable()
        {
            playerInputData.SetEnable();
            
            playerInputData.OnAnyKeyPressed += PlayerSound.KeyEnter;
            playerInputData.OnAnyKeyUnPressed += PlayerSound.KeyExit;
            playerInputData.OnAttackKeyPressed += PlayerSound.MouseClick;
        }

        private void OnDisable()
        {
            playerInputData.OnAnyKeyPressed -= PlayerSound.KeyEnter;
            playerInputData.OnAnyKeyUnPressed -= PlayerSound.KeyExit;
            playerInputData.OnAttackKeyPressed -= PlayerSound.MouseClick;
            
            playerInputData.SetDisable();
        }
    }
}
