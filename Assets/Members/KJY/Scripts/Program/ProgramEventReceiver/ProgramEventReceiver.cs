using System;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Core.Services;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Program.ProgramEventReceiver
{
    public abstract class ProgramEventReceiver<T> : MonoBehaviour
    {
        [field:Header("Event Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public ProgramEventSO MessageEvent {get; private set;}
        [field:SerializeField] public UnityEvent<T> OnEventInvoked {get; private set;}
        private IProgramManager _programManager;


        private void Start()
        {
            _programManager = ServiceLocator.GetService<IProgramManager>();
        }
        
        public void Invoke()
        {
            if (!_programManager.GetEventProgram(MessageEvent, out ProgramController[] programs)) return;
            foreach (var p in programs)
                OnEventInvoked?.Invoke(ReturnInvokeType(p));
        }


        protected abstract T ReturnInvokeType(ProgramController pc);
    }
}