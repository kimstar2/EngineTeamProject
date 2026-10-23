using System;
using System.Diagnostics.Tracing;
using _TevLib.CoreLib.EventSystem;
using _TevLib.ModuleSystem;
using _TevLib.ServiceLocatorSystem;
using Cysharp.Threading.Tasks;
using Members.KJY.Scripts.Core.Services;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Members.KJY.Scripts.Program.Application
{
    public class ApplicationOpener : MonoModule , IPointerClickHandler , IPointerEnterHandler , IPointerExitHandler
    {
        [SerializeField] private EventChannelSO eventChannel;
        private const float DelayTime = 0.4f;
        
        private ProgramInfoSO _programInfo;
        private IProgramManager _programManager;
        private bool _canOpen;
        private bool _isClicked;

        [Header("Event Setting")] [HorizontalLine(color: EColor.Gray)]
        public UnityEvent onEnter;
        public UnityEvent onExit;
        public UnityEvent onClickedExit;
        public UnityEvent onCancel;

        public event Action<ApplicationOpener> OnClicked;
        
        public void Init(ProgramInfoSO infoData)
        {
            _programInfo = infoData;
            _programManager = ServiceLocator.GetService<IProgramManager>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            OnClicked?.Invoke(this);
            _isClicked = true;
            if (_canOpen)
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
            _canOpen = false;
            onExit?.Invoke();
        }
        
        public void OpenProgram()
        {
            _canOpen = false;
            _programManager.MakeChain(_programInfo);
        }

        private async UniTask WaitDoubleClick() // 컴퓨터 데스크톱에서 더블클릭하면 어플 켜지는거인데, 얘는 더블클릭체크
        {
            _canOpen = true;
            await UniTask.Delay(TimeSpan.FromSeconds(DelayTime),cancellationToken: destroyCancellationToken); 
            _canOpen = false;
        }
    }
}