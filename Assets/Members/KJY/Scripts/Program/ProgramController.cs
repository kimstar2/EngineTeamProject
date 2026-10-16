using System;
using System.Collections.Generic;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Program.TaskBar;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program
{
    public class ProgramController : ModuleOwner
    {
        [Header("Test Setting")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private bool testMode = true;
        [ShowIf("testMode")]
        [SerializeField] private ProgramInfo TestProgramInfo; 
        [ShowIf("testMode")]
        [SerializeField] private ProgramBar ProgramBar;
        
        [field:Header("System Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public ProgramSystemBase OpenSystem { get; private set; } 
        [field:SerializeField] public ProgramSystemBase CloseSystem { get; private set; } 
        [field:SerializeField] public ProgramSystemBase MinimizeSystem { get; private set; } 
        [field:SerializeField] public MonoRect ProgramRect { get; private set; }
        
        [field:Header("Event Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public UnityEvent OnEnter { get; private set; }
        [field:SerializeField] public UnityEvent OnExit { get; private set; }
        
        public ProgramDragger ProgramDragger { get; private set; }
        public ProgramBarDataBinder ProgramBarDataBinder { get; private set; }

        protected override void InitializeModules()
        {
            base.InitializeModules();
            ProgramDragger = GetModule<ProgramDragger>();
            ProgramBarDataBinder = GetModule<ProgramBarDataBinder>();
        }

        private void Start()
        {
            if (testMode)
            {
                Init(TestProgramInfo,ProgramBar);
                ProgramBar.Init(TestProgramInfo);
            }
        }

        public void Init(ProgramInfo programInfo , ProgramBar programBar)
        {
            ProgramBarDataBinder.SetData(programInfo);

            ProgramData programData = GetProgramData(programInfo, programBar);
            
            
            foreach (IProgramInit programInit in GetComponentsInChildren<IProgramInit>())
                programInit.ProgramInit(programData);
            
            OpenSystem.ExecuteSystem();
        }
        
        private ProgramData GetProgramData(ProgramInfo programData , ProgramBar programBar)
        {
            return new(ProgramRect, gameObject, programData,programBar);
        }
    }
}