using System;
using System.Collections.Generic;
using _TevLib.ServiceLocatorSystem;
using _TevLib.ServiceLocatorSystem.TimeService;
using Members.KJY.Scripts.Core.Services;
using Members.KJY.Scripts.Program;
using UnityEngine;

namespace Members.KJY.Scripts.Core
{
    [DefaultExecutionOrder(1000)]
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Service Setting")]
        [SerializeField] private ProgramManager programManager;
        [SerializeField] private ApplicationManager applicationManager;
        [SerializeField] private FileManager fileManager;
        [SerializeField] private TimeService timeService;

        [Header("Other Setting")]
        [SerializeField] private int startFrameRate = 60;
        private void Awake()
        {
            ServiceLocator.RegisterService<IProgramManager>(programManager);
            ServiceLocator.RegisterService<IApplicationManager>(applicationManager);
            ServiceLocator.RegisterService<IFileManager>(fileManager);
            ServiceLocator.RegisterService<ITimeService>(timeService);
            ServiceLocator.RegisterService<ITickService>(timeService);
        }

        private void Start()
        {
            applicationManager.MakeDefaults();
            fileManager.MakeDefaults();
            Application.targetFrameRate = startFrameRate;
        }

        private void OnDestroy()
        {
            ServiceLocator.UnregisterService<IProgramManager>();
            ServiceLocator.UnregisterService<IApplicationManager>();
            ServiceLocator.UnregisterService<IFileManager>();
            ServiceLocator.UnregisterService<ITimeService>();
            ServiceLocator.UnregisterService<ITickService>();
        }
    }
}