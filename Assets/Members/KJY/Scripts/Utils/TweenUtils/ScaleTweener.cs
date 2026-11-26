using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Utils.TweenUtils
{
    public class ScaleTweener : TweenBase<Transform>
    {
        [Header("Scale Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private Vector3 targetScale;

        public override void ExecuteTween()
        {
            KillTween();
            CurrentTween = TargetTransform.DOScale(targetScale, Duration).SetEase(Ease);
            SetBase();
        }

        public override void ExecuteOther(Transform target)
        {
            KillTween();
            CurrentTween = target.DOScale(targetScale, Duration).SetEase(Ease);
            SetBase();
        }
    }
}
