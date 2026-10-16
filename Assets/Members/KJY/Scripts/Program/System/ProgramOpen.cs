using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program.System
{
    public class ProgramOpen : ProgramSystemBase
    {
        [Header("Tween Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private Ease tweenEase = Ease.Linear;
        [SerializeField] private float tweenDuration;
        
        private Sequence _minimizeSeq;

        public override void ExecuteSystem()
        {
            if (!ProgramData.ProgramBar.CanClick) return;
            base.ExecuteSystem();
            ProgramData.ProgramBar.Open(SystemInvoker.ProgramController);
            Open();
            SetAnchorAndPivot();
        }

        private void Open()
        {
            
            _minimizeSeq?.Kill();
            _minimizeSeq = DOTween.Sequence().
                SetEase(tweenEase).
                SetLink(gameObject).
                OnComplete(EndSystem);
            
            _minimizeSeq.Append(ProgramData.ProgramRect.Rect.DOSizeDelta(ProgramData.ProgramInfo.StartSize, tweenDuration));
            _minimizeSeq.Join(ProgramData.ProgramRect.Rect.DOAnchorPos(Bar.LastPos, tweenDuration));
        }
    }
}