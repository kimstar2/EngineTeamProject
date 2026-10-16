using DG.Tweening;
using Members.KJY.Scripts.Utils;
using NaughtyAttributes;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.System
{
    public class ProgramMinimize : ProgramSystemBase
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
            ProgramData.ProgramBar.Minimize(SystemInvoker.ProgramController,Rect.Rect.anchoredPosition);
            Minimize();
            SetAnchorAndPivot();
        }

        private void Minimize()
        {
            _minimizeSeq?.Kill();
            _minimizeSeq = DOTween.Sequence().
                SetEase(tweenEase).
                SetLink(gameObject).
                OnComplete(EndSystem);
            
            _minimizeSeq.Append(ProgramData.ProgramRect.Rect.DOSizeDelta(Vector2.zero, tweenDuration));
            _minimizeSeq.Join(ProgramData.ProgramRect.Rect.DOMove(Bar.ClosePos.Rect.position, tweenDuration));
        }
    }
}