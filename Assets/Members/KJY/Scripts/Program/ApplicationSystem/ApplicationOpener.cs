using System;
using _TevLib.ModuleSystem;
using _TevLib.ServiceLocatorSystem;
using Cysharp.Threading.Tasks;
using Members.KJY.Scripts.Core.Services;
using Members.KJY.Scripts.Program.Entry;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Members.KJY.Scripts.Program.ApplicationSystem
{
    public class ApplicationOpener : AbstractOpener
    {
        private ProgramInfoSO _programInfo;
        private ProgramEntry _programEntry;
        private IProgramManager _programManager;
        
        public void Init(ProgramInfoSO infoData, ProgramEntry entry)
        {
            _programInfo = infoData;
            _programEntry = entry;
            
            _programManager = ServiceLocator.GetService<IProgramManager>();
        }
        
        public override void OpenProgram()
        {
            
            CanOpen = false;
            _programManager.MakeChain(_programInfo , _programEntry);
        }
    }
}