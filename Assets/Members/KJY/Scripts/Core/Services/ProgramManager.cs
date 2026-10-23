using System.Collections.Generic;
using _TevLib.CoreLib;
using Members.KJY.Scripts.Check;
using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Program.TaskBar;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Members.KJY.Scripts.Core.Services
{
    public class ProgramManager : MonoBehaviour , IProgramManager , IRequiredPooling
    {
        [field:Header("Game Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public ProgramController ProgramControllerPrefab {get; private set;}
        [field:SerializeField] public ProgramBar ProgramBarPrefab {get; private set;}
        [SerializeField] private MonoRect programControllerParent;
        [SerializeField] private Transform programBarParent;
        [SerializeField] private ProgramInfoSO testProgramData;
        
        private readonly NotifyValue<ProgramController> _crtPCon = new();
        private readonly Dictionary<ProgramController, ProgramBar> _programChain = new();
        
        private readonly List<ProgramController> _enterList = new();
        
        private bool _dontSetNext;

        private void Awake() => _crtPCon.OnValueChanged += HandlePConChanged;
        private void OnDestroy() => _crtPCon.OnValueChanged -= HandlePConChanged;

        
        private void Update()
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
                Test();
        }

        [ContextMenu("Test")]
        public void Test()
        {
            MakeChain(testProgramData);
        }


        public void MakeChain(ProgramInfoSO programData) // 프로그램 키는거
        {
            ProgramController program = Instantiate(ProgramControllerPrefab,programControllerParent.transform);
            ProgramBar programBar = Instantiate(ProgramBarPrefab,programBarParent);
            
            _programChain.Add(program, programBar);
            
            program.Init(programControllerParent,programData,programBar);
            programBar.Init(programData,program);

            programBar.OnClick += HandleClicked;
            GetSystem(program, ProgramState.Enabled).OnExecute += HandleProgramEnable;
            GetSystem(program, ProgramState.Minimized).OnExecute += HandleProgramMinimized;
            GetSystem(program, ProgramState.Closed).OnExecute += HandleProgramClose;
            program.ChangeState(SystemInvoker.ProgramManager, ProgramState.Opened);
            ChangePCon(program);
        }

        #region Handle
        
        private void HandleClicked(ProgramController obj, ProgramState state)
        {
            _dontSetNext = true;
            if (state == ProgramState.Opened)
            {
                _enterList.Remove(obj);
                _enterList.Add(obj);
            }
                
            ChangePCon(obj);
            _dontSetNext = false;
        }

        private void HandlePConChanged(ProgramController prev, ProgramController next)
        {
            if (prev is not null)
                PrevStateSet(prev);
            if (!_dontSetNext)
                NextStateSet(next);
        }
        private void HandleProgramEnable(ProgramSystemBase obj) => _crtPCon.Value = obj.ProgramCon;
        
        private void HandleProgramClose(ProgramSystemBase obj)
        {
            if (_programChain.TryGetValue(obj.ProgramCon, out ProgramBar programBar))
                programBar.OnClick -= HandleClicked;
            
            GetSystem(obj.ProgramCon, ProgramState.Enabled).OnExecute -= HandleProgramEnable;
            GetSystem(obj.ProgramCon, ProgramState.Minimized).OnExecute -= HandleProgramMinimized;
            GetSystem(obj.ProgramCon, ProgramState.Closed).OnExecute -= HandleProgramClose;
            
            _programChain.Remove(obj.ProgramCon);
            
            _enterList.Remove(obj.ProgramCon);
            if (_enterList.Count == 0) return;
            var nextPCon = _enterList[^1];
            ChangePCon(nextPCon);
        }

        private void HandleProgramMinimized(ProgramSystemBase obj)
        {
            _enterList.Remove(obj.ProgramCon);
            if (_enterList.Count == 0) return;
            var nextPCon = _enterList[^1];
            ChangePCon(nextPCon);
        }

        #endregion

        #region Helper

        private ProgramSystemBase GetSystem(ProgramController program ,ProgramState state)
        {
            program.ProgramReadonlyDict.TryGetValue(state, out var system);
            return system;
        }
        
        private void PrevStateSet(ProgramController prev) // Exit
        {
            ProgramState getState = prev.SystemStateMachine.CurrentProgramState switch
            {
                ProgramState.Enabled or ProgramState.Opened => ProgramState.Disabled,
                _ => ProgramState.None
            };
            if (getState == ProgramState.None) return;
            
            prev.ChangeState(SystemInvoker.ProgramManager, getState);
        }
        
        private void NextStateSet(ProgramController next) // Enter
        {
            ProgramState getState = next.SystemStateMachine.CurrentProgramState switch
            {
                ProgramState.Disabled => ProgramState.Enabled,
                ProgramState.Minimized => ProgramState.Opened,
                _ => ProgramState.None
            };
            _enterList.Remove(next);
            _enterList.Add(next);
            if (getState == ProgramState.None) return;
                
            next.ChangeState(SystemInvoker.ProgramManager, getState);
        }
        
        private void ChangePCon(ProgramController obj)
        {
            _crtPCon.Value = obj;
        }
        
        #endregion
        
    }
}