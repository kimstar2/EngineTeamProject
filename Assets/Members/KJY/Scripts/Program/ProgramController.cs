using System;
using System.Collections.Generic;
using System.Linq;
using _TevLib.Editor.PropertyAttribute;
using _TevLib.ModuleSystem;
using Assets.Members.KJY.Scripts.Program.Func;
using DG.Tweening;
using Members.KJY.Scripts.Check;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Program.TaskBar;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
namespace Members.KJY.Scripts.Program
{
    [Serializable]
    public struct ProgramSystems
    {
        [field:SerializeField] public ProgramState ProgramState { get; private set; }
        [field:SerializeField] public SystemStateSO System { get; private set; }
    }
    public class ProgramController : ModuleOwner , IRequiredPooling
    {
        [field: Header("System Setting")] [field: HorizontalLine(color: EColor.Gray)]
        [field: SerializeField] public SystemStateMachine SystemStateMachine { get; private set; } = new();
        [field:SerializeField] public Transform ProgramParent { get; private set; }
        [field:SerializeField] public Transform FuncParent { get; private set; }
        [field:SerializeField] public MonoRect ProgramRect { get; private set; }
        
        [field:Header("Event Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public UnityEvent OnEnter { get; private set; }
        [field:SerializeField] public UnityEvent OnExit { get; private set; }

        public IReadOnlyDictionary<ProgramState, ProgramSystemBase> ProgramReadonlyDict => _programDict;
        private readonly Dictionary<ProgramState, ProgramSystemBase> _programDict = new();
        
        private List<ProgramSystems> _systems;
        private IProgramInit[] _programInits;
        
        public ProgramDragger ProgramDragger { get; private set; }
        public ProgramControllerBarDataBinder ProgramControllerBarDataBinder { get; private set; }
        public ProgramDataBinder ProgramDataBinder { get; private set; }
        public IProgramBar ProgramBar { get; private set; }
        public Sequence ProgramTween { get; set; }
        public bool IsStateChanging => ProgramTween.IsActive();
        public bool IsClosing {get; private set;}

        public Vector2 LastPosition {get; private set;}
        public Vector2 LastSize { get; private set; }
        

        protected override void InitializeModules()
        {
            base.InitializeModules();
            SystemStateMachine = new();
            
            ProgramDragger = GetModule<ProgramDragger>();
            ProgramControllerBarDataBinder = GetModule<ProgramControllerBarDataBinder>();
            ProgramDataBinder = GetModule<ProgramDataBinder>();
            _programInits = GetComponentsInChildren<IProgramInit>();
        }

        private void OnEnable()
        {
            ProgramDragger.OnBeginDragging += HandleBeginDragging;
        }

        private void OnDisable()
        {
            ProgramDragger.OnBeginDragging -= HandleBeginDragging;
        }

        public void Init(MonoRect parentRect,ProgramInfoSO programInfo,ProgramBar programBar)
        {
            ProgramData programData = new ProgramData(parentRect,ProgramRect, gameObject, programInfo);
            foreach (ProgramFunc func in programInfo.FuncPrefabs)
            {
                ProgramFunc f = Instantiate(func, FuncParent); // TODO : 풀링
                f.Init();
            }
            ProgramControllerBarDataBinder.SetData(programInfo);
            ProgramDataBinder.SetData(programInfo);
            
            IsClosing = false;
            ProgramBar = programBar;

            SystemStateMachine.Init(programData);
            
            foreach (ProgramSystems system in programInfo.ProgramSystems)
            {
                var programSystemBase = Instantiate(system.System.Program, ProgramParent);
                programSystemBase.Initialize(this, programData);
                SystemStateMachine.AddState(system.ProgramState, programSystemBase);
                _programDict.Add(system.ProgramState,programSystemBase); 
            }
            foreach (IProgramInit programInit in _programInits) 
                programInit.ProgramInit(programData);
            
            ChangeState(SystemInvoker.ProgramController,ProgramState.Opened);
        }
        
        #region Handle
        private void HandleBeginDragging()
            => ChangeState(SystemInvoker.ProgramController,ProgramState.Enabled);

        #endregion
        
        #region Helper
        
        public void ChangeState(SystemInvoker invoker, ProgramState state) => SystemStateMachine.ChangeState(invoker, state);
        public void SetLastPosition(Vector2 position) => LastPosition = position;
        public void SetLastSize(Vector2 size) => LastSize = size;

        public void OnClosing() => IsClosing = true;
        
        #endregion
    }
}