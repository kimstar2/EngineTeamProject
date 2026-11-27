using System;
using DG.Tweening;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS
{
    public class FuncAdvOS_Intro : MonoBehaviour
    {
        [Header("Intro Tween")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private CanvasGroup introGroup;
        [SerializeField] private Ease outEase;
        [SerializeField] private float outDur;
        
        [Header("Icon Tween")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private MonoImage introIcon;
        [SerializeField] private Ease ioEase;
        [SerializeField] private float ioDur;
        [SerializeField] private float ioWaitTime;
        [SerializeField] private float inWaitTime;
        [SerializeField] private float outWaitTime;
        
        [Header("Event Setting")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private UnityEvent onGame;
        [SerializeField] private UnityEvent onInMain;
        
        private Sequence _introSeq;
        
        public void StartIntro()
        {
            _introSeq?.Kill();
            _introSeq = DOTween.Sequence();
            
            
            _introSeq.AppendCallback(() => onGame?.Invoke());
            _introSeq.AppendInterval(inWaitTime);
            _introSeq.Append(introIcon.Image.DOFade(1, ioDur).SetEase(ioEase));
            _introSeq.AppendInterval(ioWaitTime);
            _introSeq.Append(introIcon.Image.DOFade(0, ioDur).SetEase(ioEase));
            _introSeq.AppendInterval(outWaitTime);
            _introSeq.Append(introGroup.DOFade(0, outDur)).SetEase(outEase);
            _introSeq.AppendCallback(() =>
            {
                introGroup.blocksRaycasts = false;
                onInMain?.Invoke();
            });
        }

        private void OnDestroy()
        {
            _introSeq?.Kill();
        }
    }
}