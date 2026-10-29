using DG.Tweening;
using Members.KJY.Scripts.Utils;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Members.KJY.Scripts.Utils.TweenUtils
{
    public class DeltaTweener : TweenBase
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
    }
}
