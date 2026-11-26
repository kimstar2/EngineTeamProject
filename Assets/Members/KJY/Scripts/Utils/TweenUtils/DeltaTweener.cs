using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Utils.TweenUtils
{
    public class DeltaTweener : TweenBase<RectTransform>
    {
        [Header("Delta Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private Vector2 targetDelta;
        
        public override void ExecuteTween()
        {
            KillTween();
            DebugLogger.Assert(Rect != null, name, "RectTransform is null");
            CurrentTween = Rect.DOSizeDelta(targetDelta,Duration);
            SetBase();
        }

        public override void ExecuteOther(RectTransform target)
        {
            KillTween();
            CurrentTween = target.DOSizeDelta(targetDelta,Duration);
            SetBase();
        }
    }
}
