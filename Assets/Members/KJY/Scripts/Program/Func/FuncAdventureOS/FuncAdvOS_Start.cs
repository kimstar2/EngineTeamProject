using System;
using DG.Tweening;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS
{
    public class FuncAdvOS_Start : MonoBehaviour
    {
        [Header("Event Setting")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private Button startButton;

        [SerializeField] private UnityEvent onMotion;
        
        [Header("Fade Tween Setting")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private CanvasGroup fadeGroup;
        [SerializeField] private float inWaitTime;
        [SerializeField] private Ease outEase;
        [SerializeField] private float outDur;
        
        [Header("Other Setting")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private CanvasGroup bgGroup;
 
        private Sequence _fadeSeq;
        private bool _isStart;

        public event Action OnEndFade;
        
        public void Init()
        {
            startButton.onClick.AddListener(HandleClick);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(HandleClick);
            _fadeSeq?.Kill();
        }

        private void HandleClick()
        {
            if (_isStart) return;
            
            _isStart = true;
            
            _fadeSeq?.Kill();
            _fadeSeq = DOTween.Sequence();
            
            fadeGroup.blocksRaycasts = true;
            bgGroup.blocksRaycasts = true;

            _fadeSeq.AppendInterval(inWaitTime);
            _fadeSeq.Append(fadeGroup.DOFade(1f, outDur)).SetEase(outEase);
            _fadeSeq.AppendCallback(() => bgGroup.alpha = 0);
            _fadeSeq.AppendInterval(outDur);
            _fadeSeq.AppendCallback(() => onMotion?.Invoke());
            _fadeSeq.Append(fadeGroup.DOFade(0f, outDur)).SetEase(outEase);
            
            if (OnEndFade != null)
                _fadeSeq.AppendCallback(OnEndFade.Invoke);
            
            fadeGroup.blocksRaycasts = false;
        }
    }
}