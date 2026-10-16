using System;
using System.Collections.Generic;
using _TevLib.CoreLib;
using _TevLib.CoreLib.EventSystem;
using JetBrains.Annotations;
using Members.KJY.Scripts.Check;
using Members.KJY.Scripts.Program.TaskBar;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Members.KJY.Scripts.Program
{
    public class ProgramManager : MonoBehaviour , IRequiredPooling
    {
        [field:Header("Game Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public ProgramController ProgramControllerPrefab {get; private set;}
        [field:SerializeField] public ProgramBar ProgramBarPrefab {get; private set;}
        [SerializeField] private Transform programControllerParent;
        [SerializeField] private Transform programBarParent;
        [SerializeField] private ProgramInfo testProgramData;
        
        private readonly Dictionary<ProgramBar, ProgramController> _programChain = new();
        private readonly NotifyValue<ProgramBar> _currentBar = new();

        private void Awake()
        {
            _currentBar.OnValueChanged += HandleCurrentBarChanged;
        }

        private void OnDestroy()
        {
            _currentBar.OnValueChanged -= HandleCurrentBarChanged;
        }

        private void Update()
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
                Test();
        }

        public void Test() => MakeChain(testProgramData);
        
        public void MakeChain(ProgramInfo programData)
        {
            ProgramController program = Instantiate(ProgramControllerPrefab,programControllerParent);
            ProgramBar programBar = Instantiate(ProgramBarPrefab,programBarParent);
            
            _programChain.Add(programBar, program);
            Subscribe(programBar, program);
            
            program.Init(programData,programBar);
            programBar.Init(programData);
        }

        #region Handle
        
        private void HandleCurrentBarChanged(ProgramBar prev, ProgramBar next)
        {
            ExitProgram(prev);
            EnterProgram(next);
        }

        private void HandleProgramOpen(ProgramBar bar,SystemInvoker invoker)
        {
            EnterProgram(bar);
            if (SetAndCheck(bar, invoker)) return;
            if (_programChain.TryGetValue(bar, out var program))
            {
                program.OpenSystem.ExecuteSystem();
            }
        }

        private void HandleChangedBar(ProgramBar programBar)
        {
            _currentBar.Value = programBar;
        }
        
        private void HandleProgramMinimize(ProgramBar bar,SystemInvoker invoker)
        {
            ExitProgram(bar);
            if (SetAndCheck(bar, invoker)) return;
            if (_programChain.TryGetValue(bar, out var program))
            {
                program.MinimizeSystem.ExecuteSystem();
            }
        }

        private void HandleProgramClose(ProgramBar bar)
        {
            if (!_programChain.TryGetValue(bar, out var program)) return;
            UnSubscribe(bar, program);
            _programChain.Remove(bar);
        }

        #endregion
        
        
        #region Helper

        private void Subscribe(ProgramBar programBar , ProgramController program)
        {
            programBar.OnClose += HandleProgramClose;
            programBar.OnMinimize += HandleProgramMinimize;
            programBar.OnOpen += HandleProgramOpen;
            
            programBar.OnSystemInvoked += HandleChangedBar;
            program.ProgramDragger.OnBeginDragging += HandleChangedBar;
        }
        
        private void UnSubscribe(ProgramBar bar, ProgramController program)
        {
            bar.OnClose -= HandleProgramClose;
            bar.OnMinimize -= HandleProgramMinimize;
            bar.OnOpen -= HandleProgramOpen;
            
            bar.OnSystemInvoked -= HandleChangedBar;
            program.ProgramDragger.OnBeginDragging -= HandleChangedBar;
        }
        
        private bool SetAndCheck(ProgramBar bar, SystemInvoker invoker)
        {
            _currentBar.Value = bar;
            if (invoker != SystemInvoker.ProgramBar) return true;
            return false;
        }
        
        private void ExitProgram(ProgramBar prev)
        {
            if (prev is null) return;
            if (_programChain.TryGetValue(prev, out var program))
            {
                program.OnExit?.Invoke();
            }
            prev.onExit?.Invoke();
        }

        private void EnterProgram([NotNull] ProgramBar next)
        {
            if (_programChain.TryGetValue(next, out var program))
            {
                program.transform.SetAsLastSibling();
                program.OnEnter?.Invoke();
            }

            next.onEnter?.Invoke();
        }

        #endregion
    }
}