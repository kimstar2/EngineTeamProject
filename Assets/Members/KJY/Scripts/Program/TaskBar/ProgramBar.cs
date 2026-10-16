using System;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Check;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Program.TaskBar
{
    public class ProgramBar : ModuleOwner , IRequiredPooling
    {
        [field:SerializeField] public MonoRect ClosePos {get; private set;}
        [ShowNonSerializedField] private ProgramState _currentProgramState;
        public Vector3 LastPos { get; private set; }
        
        public event Action<ProgramBar,SystemInvoker> OnOpen;
        public event Action<ProgramBar,SystemInvoker> OnMinimize;
        public event Action<ProgramBar> OnSystemInvoked;
        public event Action<ProgramBar> OnClose;

        public UnityEvent onEnter;
        public UnityEvent onExit;
        
        public ProgramBarDataBinder ProgramBarDataBinder { get; private set; }
        public bool CanClick { get; private set; } = true;

        protected override void InitializeModules()
        {
            base.InitializeModules();
            ProgramBarDataBinder = GetModule<ProgramBarDataBinder>();
        }
        
        public void Init(ProgramInfo programData)
        {
            ProgramBarDataBinder.SetData(programData);
            LastPos = programData.StartPosition;
        }

        public void ClickToggle()
        {
            if (!CanClick) return;
            switch (_currentProgramState)
            {
                case ProgramState.Opened:
                    Minimize(SystemInvoker.ProgramBar,LastPos);
                    break;
                case ProgramState.Minimized:
                    Open(SystemInvoker.ProgramBar);
                    break;
            }
        }

        public void NowCanClick() => CanClick = true;
        
        #region Events
        
        public void SystemInvoked()
        {
            OnSystemInvoked?.Invoke(this);
            CanClick = false;
        }

        public void Open(SystemInvoker invoker)
        {
            OnOpen?.Invoke(this,invoker);
            _currentProgramState = ProgramState.Opened;
        }
        
        public void Minimize(SystemInvoker invoker, Vector2 lastPos)
        {
            LastPos = lastPos;
            OnMinimize?.Invoke(this,invoker);
            _currentProgramState = ProgramState.Minimized;
        }
        
        public void Close()
        {
            OnClose?.Invoke(this);
            _currentProgramState = ProgramState.Closed;
        }
        
        #endregion
        
    }
}