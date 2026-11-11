using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Program.TaskBar
{
    public class WindowBar : ModuleOwner
    {
        [field:SerializeField] public MonoRect ClosePos {get; private set;}
        [field:SerializeField] public ProgramInfoSO StartInfo {get; private set;}
        
        [field:Header("Event Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public UnityEvent OnEnter {get; private set;}
        [field:SerializeField] public UnityEvent OnExit {get; private set;}
        
        public ProgramBarDataBinder ProgramBarDataBinder { get; private set; }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            ProgramBarDataBinder = GetModule<ProgramBarDataBinder>();
            
            ProgramBarDataBinder.SetData(StartInfo);
        }
        
        // public void WindowToggle
    }
}