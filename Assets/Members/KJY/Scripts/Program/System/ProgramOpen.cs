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
        
        public override void ExecuteSystem()
        {
            base.ExecuteSystem();
            SetAnchorAndPivot();
            Open();
        }

        private void Open()
        {
            ProgramCon.ProgramTween?.Kill();
            ProgramCon.ProgramTween = DOTween.
                Sequence().
                SetEase(tweenEase).
                SetLink(gameObject).
                SetAutoKill(true);;
            
            ProgramCon.ProgramTween.Append(ProgramData.ProgramRect.Rect.DOSizeDelta(ProgramCon.LastSize, tweenDuration));
            ProgramCon.ProgramTween.Join(ProgramData.ProgramRect.Rect.DOAnchorPos(ProgramCon.LastPosition, tweenDuration));
            
            ProgramCon.ProgramRect.transform.SetAsLastSibling();
            ProgramCon.ProgramBar.OnEnter?.Invoke();
            ProgramCon.OnEnter?.Invoke();
        }
    }
}