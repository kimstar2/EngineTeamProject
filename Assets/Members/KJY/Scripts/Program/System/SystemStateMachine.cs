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
        public void Init(ProgramData programData)
        {
            ProgramData = programData;
        }
        
        public void AddState(ProgramState programState, ProgramSystemBase programSystemState)
        {
            _states.Add(programState, programSystemState);
        }
        
        public void ChangeState(SystemInvoker invoker,ProgramState state)
        {
            if (!_states.TryGetValue(state, out var systemState)) return;
            if (CurrentSystemState is {ProgramCon:{IsClosing:true}}) return;
            LastInvoker = invoker;
            CurrentSystemState?.ExitSystem();
            CurrentSystemState = systemState;
            CurrentSystemState?.ExecuteSystem();
            CurrentProgramState = state;
        }
    }
}