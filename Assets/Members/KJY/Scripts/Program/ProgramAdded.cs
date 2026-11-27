using System;
using DG.Tweening;
using JetBrains.Annotations;
using Members.KJY.Scripts.Utils.Mono;
using Members.KJY.Scripts.Utils.TweenUtils;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program
{
    public class ProgramAdded : MonoBehaviour
    {
        [SerializeField] private MonoRect targetRect;
        [SerializeField] private MonoImage iconImage;
        [SerializeField] private MonoTMP_UGUI programAdded;
        [SerializeField] private MonoTMP_UGUI programName;

        [Header("Tween Setting")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private TweenMotion onMotion;
        [SerializeField] private float timeGap;
        [SerializeField] private TweenMotion offMotion;
        [SerializeField] private Vector2 toPos;
        [SerializeField] private Vector2 originPos;
        private Sequence _addedSeq;
        
        public void Set(Sprite img, string added, string prgName)
        {
            iconImage.SetImage(img);
            programAdded.SetText(added);
            programName.SetText(prgName);
            
            _addedSeq?.Kill();
            _addedSeq = DOTween.Sequence();
            _addedSeq.Append(targetRect.Rect.DOAnchorPos(toPos, onMotion.duration).SetEase(onMotion.ease));
            _addedSeq.AppendInterval(timeGap);
            _addedSeq.Append(targetRect.Rect.DOAnchorPos(originPos, offMotion.duration).SetEase(offMotion.ease));
            _addedSeq.AppendCallback(() => Destroy(gameObject));
        }

        private void OnDestroy()
        {
            _addedSeq?.Kill();
        }
    }
}