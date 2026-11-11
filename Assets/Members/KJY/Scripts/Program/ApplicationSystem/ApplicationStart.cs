using _TevLib.ModuleSystem;
using DG.Tweening;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.ApplicationSystem
{
    public class ApplicationStart : MonoModule
    {
        [field: SerializeField] public MonoRect StartRect {get; private set;} 
        
        [Header("Tween Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private float tweenDur = 0.15f;
        private Tween _motionTween;
        public void Open(Vector2 size)
        {
            _motionTween?.Kill();
            _motionTween = StartRect.Rect.DOSizeDelta(size,tweenDur).SetEase(Ease.OutSine);
        }

        public void Close()
        {
            _motionTween?.Kill();
            _motionTween = StartRect.Rect.DOSizeDelta(Vector2.zero,tweenDur).SetEase(Ease.InSine);
        }
    }
}