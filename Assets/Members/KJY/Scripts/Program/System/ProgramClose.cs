using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Members.KJY.Scripts.Utils;
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
        
        public override async void ExecuteSystem()
        {
            try
            {
                if (ProgramCon.IsStateChanging) return;
                SetAnchorAndPivot();
                ProgramCon.OnClosing();
                await Close();
                base.ExecuteSystem();
                EndSystem();
            }
            catch (OperationCanceledException) { }
            catch (Exception e)
            {
                DebugLogger.LogException(e);
            }
        }

        private async UniTask Close()
        {
            ProgramCon.ProgramTween?.Kill();
            ProgramCon.ProgramTween = DOTween.Sequence().SetLink(gameObject);
            await ProgramCon.ProgramTween.Append(Rect.Rect.DOSizeDelta(Vector2.zero, tweenDuration).
                SetEase(tweenEase)).
                ToUniTask(cancellationToken:destroyCancellationToken);
        }

        private void EndSystem()
        {
            ProgramCon.ProgramTween?.Kill();
            Destroy(ProgramCon.ProgramBar.GameObject);
            Destroy(ProgramData.GameObject);
        }
    }
}