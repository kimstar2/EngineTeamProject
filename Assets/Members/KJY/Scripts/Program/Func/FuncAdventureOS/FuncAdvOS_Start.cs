using System;
using DG.Tweening;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS
{
    public class FuncAdvOS_Start : MonoBehaviour
    {
        [Header("Event Setting")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private Button startButton;
        
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

        public void Init()
        {
            startButton.onClick.AddListener(HandleClick);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveListener(HandleClick);
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
            _fadeSeq.Append(fadeGroup.DOFade(0f, outDur)).SetEase(outEase);
            fadeGroup.blocksRaycasts = false;
        }
    }
}