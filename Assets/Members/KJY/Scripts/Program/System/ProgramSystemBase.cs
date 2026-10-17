using System;
using DG.Tweening;
using Members.KJY.Scripts.Program.TaskBar;
using Members.KJY.Scripts.Utils;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program.System
{
    public abstract class ProgramSystemBase : MonoBehaviour
    {
        [field: SerializeField] protected float ChangeTime { get; private set; } = 0.2f;
        [field:SerializeField] protected AnchorUtilStruct AnchorUtil {get; private set;}
        public ProgramData ProgramData {get; private set;}
        public MonoRect Rect => ProgramData.ProgramRect;
        public ProgramController ProgramCon {get; private set;}
        
        public event Action<ProgramSystemBase> OnExecute;
        public event Action<ProgramSystemBase> OnExit;
        
        public virtual void Initialize(ProgramController programCon, ProgramData programData)
        {
            ProgramData = programData;
            ProgramCon = programCon;
        }
        
        public virtual void ExecuteSystem()
        {
            OnExecute?.Invoke(this);
        }

        protected void SetAnchorAndPivot()
        {
            ProgramData.ProgramRect.OnlySetAnchorAndPivot(AnchorUtil.MinAnchor,
                AnchorUtil.MaxAnchor,
                AnchorUtil.Pivot);
        }

        public virtual void ExitSystem()
        {
            OnExit?.Invoke(this);
        }
    }
}