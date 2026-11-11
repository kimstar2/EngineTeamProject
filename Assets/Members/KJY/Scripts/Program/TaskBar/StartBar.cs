using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Program.ApplicationSystem;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Program.TaskBar
{
    public class StartBar : ModuleOwner
    {
        [field:SerializeField] public MonoRect ClosePos {get; private set;}
        [field:SerializeField] public ProgramInfoSO StartInfo {get; private set;}
        
        [field:Header("Event Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public UnityEvent OnEnter {get; private set;}
        [field:SerializeField] public UnityEvent OnExit {get; private set;}
        public bool IsToggled {get; private set;}
        
        public ApplicationStart ApplicationStart {get; private set;}
        public ProgramBarDataBinder ProgramBarDataBinder { get; private set; }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            ProgramBarDataBinder = GetModule<ProgramBarDataBinder>();
            ApplicationStart = GetModule<ApplicationStart>();
            
            ProgramBarDataBinder.SetData(StartInfo);
        }

        public void WindowToggle()
        {
            IsToggled = !IsToggled;

            if (IsToggled)
                ApplicationStart.Open(StartInfo.StartSize);
            else
                ApplicationStart.Close();
        }
    }
}