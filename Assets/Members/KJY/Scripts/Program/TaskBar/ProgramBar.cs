using System;
using System.Collections.Generic;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Check;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Program.TaskBar
{
    public class ProgramBar : ModuleOwner , IRequiredPooling , IProgramBar
    {
        [field:SerializeField] public MonoRect ClosePos {get; private set;}
        
        [field:Header("Event Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public UnityEvent OnEnter {get; private set;}
        [field:SerializeField] public UnityEvent OnExit {get; private set;}

        public ProgramBarDataBinder ProgramBarDataBinder { get; private set; }
        public ProgramController ProgramController { get; private set; }
        public GameObject GameObject => gameObject;
        
        public Vector3 LastPos { get; private set; }
        public bool CanClick { get; private set; } = true;

        public event Action<ProgramController,ProgramState> OnClick;
        

        protected override void InitializeModules()
        {
            base.InitializeModules();
            ProgramBarDataBinder = GetModule<ProgramBarDataBinder>();
        }
        
        public void Init(ProgramInfoSO programData , ProgramController controller)
        {
            ProgramController = controller;
            ProgramBarDataBinder.SetData(programData);
            LastPos = programData.StartPosition;
        }

        public void ClickToggle()
        {
            if (!CanClick) return;
            
            ProgramState afterState = ProgramController.SystemStateMachine.CurrentProgramState switch
            {
                ProgramState.Disabled => ProgramState.Enabled,
                ProgramState.Minimized => ProgramState.Opened,
                ProgramState.Enabled => ProgramState.Minimized,
                ProgramState.Opened => ProgramState.Minimized,
                _ => ProgramState.None
            };
            
            OnClick?.Invoke(ProgramController,afterState);
            
            if (afterState != ProgramState.None)
                ProgramController.ChangeState(SystemInvoker.ProgramBar, afterState);
        }
    }

    public interface IProgramBar
    {
        UnityEvent OnEnter {get;}
        UnityEvent OnExit {get;}
        
        GameObject GameObject { get; }
        MonoRect ClosePos { get; }
        
        
    }
}