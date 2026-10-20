using System;
using Cysharp.Threading.Tasks;
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
        
        public override async void ExecuteSystem()
        {
            try
            {
                if (!ProgramCon.IsStateChanging)
                {
                    ProgramCon.SetLastPosition(ProgramCon.ProgramRect.Rect.anchoredPosition);
                    SetAnchorAndPivot();
                }
                ProgramCon.ProgramBar.OnExit?.Invoke();
                await Minimize(ProgramCon);
                base.ExecuteSystem();
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                DebugLogger.LogException(e);
            }
        }

        private async UniTask Minimize(ProgramController controller)
        {
            ProgramCon.ProgramTween?.Kill();
            ProgramCon.ProgramTween = DOTween.
                Sequence().
                SetEase(tweenEase).
                SetLink(gameObject).
                SetAutoKill(true);

            ProgramCon.ProgramTween.Append(ProgramData.ProgramRect.Rect.DOSizeDelta(Vector2.zero, tweenDuration));
            ProgramCon.ProgramTween.Join(ProgramData.ProgramRect.Rect.DOMove(controller.ProgramBar.ClosePos.Rect.position, tweenDuration));
            await UniTask.Delay(TimeSpan.FromSeconds(ChangeTime),cancellationToken:destroyCancellationToken);
        }
    }
}