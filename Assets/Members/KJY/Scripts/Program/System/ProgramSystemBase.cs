using System;
using Members.KJY.Scripts.Program.TaskBar;
using Members.KJY.Scripts.Utils;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program.System
{
    public abstract class ProgramSystemBase : MonoProgramInit
    {
        [field:SerializeField] protected AnchorUtilStruct AnchorUtil {get; private set;}
        public ProgramBar Bar => ProgramData.ProgramBar;
        public MonoRect Rect => ProgramData.ProgramRect;
        public ProgramInfo Info => ProgramData.ProgramInfo;
        
        public virtual void ExecuteSystem() // base 안쓰면 그전 시스템 앵커, 피봇 따라감
        {
            Bar.SystemInvoked();
        }

        protected void SetAnchorAndPivot()
        {
            ProgramData.ProgramRect.OnlySetAnchorAndPivot(AnchorUtil.MinAnchor,
                AnchorUtil.MaxAnchor,
                AnchorUtil.Pivot);
        }
        
        protected virtual void EndSystem()
        {
            Bar.NowCanClick();
        }
    }
}