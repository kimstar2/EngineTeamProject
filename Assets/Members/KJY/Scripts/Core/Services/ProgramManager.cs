using System;
using System.Collections.Generic;
using System.Linq;
using _TevLib.CoreLib;
using _TevLib.CoreLib.EventSystem;
using Members.KJY.Scripts.Check;
using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.Error;
using Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Program.TaskBar;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Members.KJY.Scripts.Core.Services
{
    public class ProgramManager : MonoBehaviour , IProgramManager , IRequiredPooling
    {
        [SerializeField] private EventChannelSO eventChannel;
        [SerializeField] private Key testKey;
        [field:Header("Game Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public ProgramController ProgramControllerPrefab {get; private set;}
        [field:SerializeField] public ProgramBar ProgramBarPrefab {get; private set;}
        [SerializeField] private MonoRect backProgramTask;
        [SerializeField] private MonoRect frontProgramTask;
        [SerializeField] private Transform programBarParent;
        [SerializeField] private ProgramInfoSO testProgramData;

        [Header("Error Setting")] 
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private ProgramInfoSO errorMessage;
        
        [Header("Message Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private ProgramInfoSO messageInfo;
        
        private readonly NotifyValue<ProgramController> _crtPCon = new();
        private readonly Dictionary<ProgramController, ProgramBar> _programChain = new();
        
        private readonly List<ProgramController> _enterList = new();
        private readonly HashSet<UnityEngine.Object> _onlyOneSet = new();
        
        private bool _dontSetNext;

        private void Awake()
        {
            _crtPCon.OnValueChanged += HandlePConChanged;
            eventChannel.AddListener<OnError>(HandleError);
            eventChannel.AddListener<OnMessage>(HandleMessage);
        }

        private void HandleMessage(OnMessage msgEvt)
        {
            MessageEntry messageEntry = ScriptableObject.CreateInstance<MessageEntry>();
            messageEntry.SetAudio(msgEvt.SoundClips);
            if (msgEvt.Systems.Count > 0)
                messageEntry.SetMessageData(msgEvt.AnimCon,msgEvt.MessageGroup,msgEvt.EventDict,msgEvt.Systems);
            else
                messageEntry.SetMessageData(msgEvt.AnimCon,msgEvt.MessageGroup,msgEvt.EventDict);
            
            messageEntry.SetSpawnPos(msgEvt.Position);
            MakeChain(messageInfo,messageEntry);
        }

        private void HandleError(OnError errorEvt)
        {
            ErrorEntry error = ScriptableObject.CreateInstance<ErrorEntry>();
            error.SetErrorMessage(errorEvt.ErrorMessage,errorEvt.RenderOrder);
            MakeChain(errorMessage,error);
        }

        private void OnDestroy()
        {
            _crtPCon.OnValueChanged -= HandlePConChanged;
            eventChannel.RemoveListener<OnError>(HandleError);
            eventChannel.RemoveListener<OnMessage>(HandleMessage);
        }


        private void Update()
        {
            if (Keyboard.current[testKey].wasPressedThisFrame)
                Test();
        }

        [ContextMenu("Test")]
        public void Test()
        {
            // MakeChain(testProgramData);
        }


        public ProgramController MakeChain(ProgramInfoSO programData, ProgramEntry entry) // 프로그램 키는거
        {
            var limitKey = entry.GetInstanceLimitKey(programData);
            if (limitKey != null && !_onlyOneSet.Add(limitKey))
            {
                ErrorEntry error = ScriptableObject.CreateInstance<ErrorEntry>();
                error.SetErrorMessage($"{entry.GetProgramName(programData)} 창이 이미 열려 있습니다. 기존 창을 이용해 주세요.",entry.RenderOrder);
                MakeChain(errorMessage,error);
                return null;
            }


            MonoRect renderTask = entry.RenderOrder switch
            {
                ProgramRenderOrder.BackProgramTask => backProgramTask,
                ProgramRenderOrder.FrontProgramTask => frontProgramTask,
                _ => throw new ArgumentOutOfRangeException()
            };
            
            ProgramController program = Instantiate(ProgramControllerPrefab,renderTask.transform);
            ProgramBar programBar = Instantiate(ProgramBarPrefab,programBarParent);
            
            _programChain.Add(program, programBar);
            
            program.Init(renderTask,programData,programBar,entry);
            programBar.Init(programData,program,entry);

            programBar.OnClick += HandleClicked;
            if (program.ProgramReadonlyDict.TryGetValue(ProgramState.Enabled, out var enabledSystem))
                enabledSystem.OnExecute += HandleProgramEnable;
            if (program.ProgramReadonlyDict.TryGetValue(ProgramState.Minimized, out var minimizedSystem))
                minimizedSystem.OnExecute += HandleProgramMinimized;
            if (program.ProgramReadonlyDict.TryGetValue(ProgramState.Closed, out var closedSystem))
                closedSystem.OnExecute += HandleProgramClose;
            
            program.ChangeState(SystemInvoker.ProgramManager, ProgramState.Opened);
            ChangePCon(program);
            
            return program;
        }

        public bool GetEventProgram(ProgramEventSO eventSo , out ProgramController[] program)
        { 
            program = _programChain.Keys.Where(pc => pc.MyEntry.UseProgramEvent && pc.MyEntry.Event?.Contains(eventSo) == true).ToArray();
            return program.Length > 0;
        }

        public void MakeChain(ProgramInfoSO programData)
        {
            // TODO : TestMake
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
            
            if (obj.ProgramCon.ProgramReadonlyDict.TryGetValue(ProgramState.Enabled, out var enabledSystem))
                enabledSystem.OnExecute -= HandleProgramEnable;
            if (obj.ProgramCon.ProgramReadonlyDict.TryGetValue(ProgramState.Minimized, out var minimizedSystem))
                minimizedSystem.OnExecute -= HandleProgramMinimized;
            if (obj.ProgramCon.ProgramReadonlyDict.TryGetValue(ProgramState.Closed, out var closedSystem))
                closedSystem.OnExecute -= HandleProgramClose;
            
            _onlyOneSet.Remove(obj.ProgramData.Entry.GetInstanceLimitKey(obj.ProgramData.ProgramInfo));
            
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