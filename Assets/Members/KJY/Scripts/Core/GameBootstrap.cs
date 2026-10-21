using System;
using System.Collections.Generic;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Program;
using UnityEngine;

namespace Members.KJY.Scripts.Core
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private ProgramManager programManager;
        private void Awake()
        {
            ServiceLocator.RegisterService<IProgramManager>(programManager);
        }

        private void OnDestroy()
        {
            ServiceLocator.UnregisterService<IProgramManager>();
        }
    }
}