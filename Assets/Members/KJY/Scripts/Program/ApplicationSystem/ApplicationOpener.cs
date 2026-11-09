using System;
using _TevLib.ModuleSystem;
using _TevLib.ServiceLocatorSystem;
using Cysharp.Threading.Tasks;
using Members.KJY.Scripts.Core.Services;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Members.KJY.Scripts.Program.ApplicationSystem
{
    public class ApplicationOpener : AbstractOpener
    {
        private ProgramInfoSO _programInfo;
        private IProgramManager _programManager;
        
        public void Init(ProgramInfoSO infoData)
        {
            _programInfo = infoData;
            _programManager = ServiceLocator.GetService<IProgramManager>();
        }
        
        public override void OpenProgram()
        {
            CanOpen = false;
            _programManager.MakeChain(_programInfo);
        }
    }
}