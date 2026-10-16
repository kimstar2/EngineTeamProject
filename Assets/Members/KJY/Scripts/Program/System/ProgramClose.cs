using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program.System
{
    public class ProgramClose : ProgramSystemBase
    {
        [Header("Tween Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private Ease tweenEase = Ease.Linear;
        [SerializeField] private float tweenDuration;
        
        private Tween _minimizeTween;

        public override void ExecuteSystem()
        {
            if (!ProgramData.ProgramBar.CanClick) return;
            base.ExecuteSystem();
            ProgramData.ProgramBar.Close();
            Close();
            SetAnchorAndPivot();
        }

        private void Close()
        {
            _minimizeTween?.Kill();
            _minimizeTween = ProgramData.ProgramRect.Rect.DOSizeDelta(Vector2.zero, tweenDuration).
                SetEase(tweenEase).
                SetLink(gameObject).
                OnComplete(EndSystem);
        }

        protected override void EndSystem()
        {
            base.EndSystem();
            Destroy(Bar.gameObject);
            Destroy(ProgramData.GameObject);
        }
    }
}