using System;
using System.Collections.Generic;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Core.Services;
using Members.KJY.Scripts.Program;
using UnityEngine;

namespace Members.KJY.Scripts.Core
{
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private ProgramManager programManager;
        [SerializeField] private ApplicationManager applicationManager;
        [SerializeField] private FileManager fileManager;
        private void Awake()
        {
            ServiceLocator.RegisterService<IProgramManager>(programManager);
            ServiceLocator.RegisterService<IApplicationManager>(applicationManager);
            ServiceLocator.RegisterService<IFileManager>(fileManager);
        }

        private void Start()
        {
            applicationManager.MakeDefaults();
            fileManager.MakeDefaults();
        }

        private void OnDestroy()
        {
            ServiceLocator.UnregisterService<IProgramManager>();
            ServiceLocator.UnregisterService<IApplicationManager>();
            ServiceLocator.UnregisterService<IFileManager>();
        }
    }
}