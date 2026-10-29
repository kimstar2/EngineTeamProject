using Assets.Members.KJY.Scripts.Utils.Mono;
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
    public abstract class TweenBase : MonoBase<Transform>, ITweenable
    {
        [field:Header("Default Tween Setting")]
        [field:HorizontalLine(color: EColor.Gray)]
        [field:SerializeField] protected float Duration { get; private set; }
        [field:SerializeField] protected Ease Ease { get; private set; }

        [Header("ID Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private bool usingID;

        [ShowIf("usingID")]
        [SerializeField] private Transform transformID;
        private int _id;

        public Tween CurrentTween { get; protected set; }

        public Transform TargetTransform => compo;
        public RectTransform Rect { get; protected set; }

        public override void MonoInit()
        {
            base.MonoInit();

            _id = transformID.GetHashCode();

            if (TargetTransform is RectTransform rect)
                Rect = rect;
        }

        public abstract void ExecuteTween();

        public void KillTween()
        {
            if (usingID)
                DOTween.Kill(_id);
            else
                CurrentTween?.Kill();

            CurrentTween = null;
        }

        protected void SetBase()
        {
            CurrentTween.SetId(_id).SetEase(Ease);
        }
    }
}
