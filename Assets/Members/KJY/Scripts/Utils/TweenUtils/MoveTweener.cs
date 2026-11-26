using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Utils.TweenUtils
{
    public class MoveTweener : TweenBase<Transform>
    {
        [Header("Scale Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private Vector3 targetPos;

        public override void ExecuteTween()
        {
            KillTween();
            CurrentTween = TargetTransform.DOMove(targetPos, Duration).SetEase(Ease);
            SetBase();
        }

        public override void ExecuteOther(Transform target)
        {
            KillTween();
            CurrentTween = target.DOMove(targetPos, Duration).SetEase(Ease);
            SetBase();
        }
    }
}