using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Members.KJY.Scripts.Utils.TweenUtils
{
    public class ScaleTweener : TweenBase
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
    }
}
