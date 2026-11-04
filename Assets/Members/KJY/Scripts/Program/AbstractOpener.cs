using System;
using _TevLib.ModuleSystem;
using Cysharp.Threading.Tasks;
using Members.KJY.Scripts.Program.ApplicationSystem;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Members.KJY.Scripts.Program
{
    public abstract class AbstractOpener : MonoModule , IPointerClickHandler , IPointerEnterHandler , IPointerExitHandler
    {
        private const float DelayTime = 0.4f;
        
        protected bool CanOpen;
        private bool _isClicked;
        
        [Header("Event Setting")]
        [HorizontalLine(color: EColor.Gray)]
        public UnityEvent onEnter;
        public UnityEvent onExit;
        public UnityEvent onClickedExit;
        
        public event Action<AbstractOpener> OnClicked;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            OnClicked?.Invoke(this);
            _isClicked = true;
            if (CanOpen)
            {
                OpenProgram();
                return;
            }
            WaitDoubleClick().Forget();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isClicked)
                onClickedExit?.Invoke();
            else
                onExit?.Invoke();
        }
        
        public void ExitClick()
        {
            _isClicked = false;
            CanOpen = false;
            onExit?.Invoke();
        }
        
        public abstract void OpenProgram();
        
        private async UniTask WaitDoubleClick() // 컴퓨터 데스크톱에서 더블클릭하면 어플 켜지는거인데, 얘는 더블클릭체크
        {
            CanOpen = true;
            await UniTask.Delay(TimeSpan.FromSeconds(DelayTime),cancellationToken: destroyCancellationToken); 
            CanOpen = false;
        }
    }
}