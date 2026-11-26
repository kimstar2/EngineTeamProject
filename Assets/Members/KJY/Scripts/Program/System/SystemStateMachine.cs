using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program.System
{
    [Serializable]
    public class SystemStateMachine
    {
        [field:SerializeField] public ProgramState CurrentProgramState {get; private set;}
        [field:SerializeField] public ProgramSystemBase CurrentSystemState {get; private set;}
        private readonly Dictionary<ProgramState, ProgramSystemBase> _states = new();
        public ProgramData ProgramData {get; private set;}
        public SystemInvoker LastInvoker {get; private set;}
        private ProgramController _controller;

        public void Init(ProgramData programData, ProgramController controller)
        {
            ProgramData = programData;
            _controller = controller;
        }
        
        public void AddState(ProgramState programState, ProgramSystemBase programSystemState)
        {
            _states.Add(programState, programSystemState);
        }
        
        public void ChangeState(SystemInvoker invoker,ProgramState state)
        {
            if (CurrentProgramState == ProgramState.Minimized && state == ProgramState.Enabled) return;
            
            if (_controller == null || _controller.IsClosing) return;
            if (!_states.TryGetValue(state, out var systemState) && state != ProgramState.Opened) return;
            LastInvoker = invoker;
            CurrentSystemState?.ExitSystem();
            CurrentSystemState = systemState;
            if (systemState != null)
                systemState.ExecuteSystem();
            else
                _controller.OpenWithoutAnimation();
            CurrentProgramState = state;
        }
    }
}