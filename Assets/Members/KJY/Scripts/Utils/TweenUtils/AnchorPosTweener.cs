using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Utils.TweenUtils
{
    public class AnchorPosTweener : TweenBase<RectTransform>
    {
        [Header("Delta Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private Vector2 targetAnchorPos;
        
        public override void ExecuteTween()
        {
            KillTween();
            DebugLogger.Assert(Rect != null, name, "RectTransform is null");
            CurrentTween = Rect.DOAnchorPos(targetAnchorPos,Duration).SetEase(Ease);
            SetBase();
        }

        public override void ExecuteOther(RectTransform target)
        {
            KillTween();
            DebugLogger.Assert(Rect != null, name, "RectTransform is null");
            CurrentTween = target.DOAnchorPos(targetAnchorPos,Duration).SetEase(Ease);
            SetBase();
        }
    }
}