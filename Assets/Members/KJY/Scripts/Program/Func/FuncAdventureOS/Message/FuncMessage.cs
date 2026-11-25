using System;
using System.Threading.Tasks;
using _TevLib.HashDataSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Febucci.UI;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Utils;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class FuncMessage : ProgramFunc
    {
        [SerializeField] private Animator charAnim;
        [SerializeField] private MonoTMP_UGUI characterName;
        [SerializeField] private TypewriterByCharacter messageTmp;
        
        [Header("ReSize Setting")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private Vector2 minSize;
        [SerializeField] private float minYSize = 70f;
        [SerializeField] private Ease reSizeEase;
        [SerializeField] private float reSizeDur;

        
        private ProgramController _pc;
        private ProgramSystemBase _closeToken;
        private string _crtMessage;
        private bool _moveNext;
        
        private RectTransform _parentRect;
        private Tween _textTween;
        private TextAnimator_TMP _textAnimTMP; 
        private TMP_Text _tmpText;

        public override void Init()
        {
            base.Init();
            _textAnimTMP = (TextAnimator_TMP)messageTmp.TextAnimator;
            _parentRect = _pc.ProgramRect.Rect.parent as RectTransform;
            _tmpText = _textAnimTMP.TMProComponent;
            // _pc.ProgramRect.Rect.pivot.
            
            messageTmp.onCharacterVisible.AddListener(HandleResizeTmpMesh);
            messageTmp.onTextShowed.AddListener(HandleTextShowed);
        }

        private void HandleTextShowed()
        {
            HandleResizeTmpMesh();
            UIUtils.AdjustmentUI(_parentRect, _pc.ProgramRect.Rect);
        }

        private void HandleResizeTmpMesh(char _ = ' ')
        {
            int visibleIndex = _textAnimTMP.latestCharacterShown.index;
            var lastCharacter = _tmpText.textInfo.characterInfo[visibleIndex];
            int end = Mathf.Min(lastCharacter.index + lastCharacter.stringLength, _tmpText.text.Length);

            string visibleText = _tmpText.text[..end];
            Vector2 preferredSize = _tmpText.GetPreferredValues(visibleText);

            Vector2 applyMinSize = minSize + new Vector2(0,characterName.TMP.GetPreferredValues().y);
            
            if (preferredSize.y <= applyMinSize.y - minYSize)
                preferredSize.y = 0f;
            else
                preferredSize.y -= applyMinSize.y - minYSize;

            _textTween?.Kill();
            _textTween = DOTween.To(() => _pc.ProgramRect.Rect.sizeDelta,
                x => _pc.ProgramRect.Rect.sizeDelta = x,
                applyMinSize + preferredSize, reSizeDur).SetEase(reSizeEase);
            
            UIUtils.AdjustmentUI(_parentRect, _pc.ProgramRect.Rect);
        }

        private void OnDestroy()
        {
            messageTmp.onCharacterVisible.RemoveListener(HandleResizeTmpMesh);
            messageTmp.onTextShowed.RemoveListener(HandleTextShowed);
            _textTween?.Kill();
        }

        public void SetData(MessageEntry entry, ProgramController pc, ProgramSystemBase closeToken)
        {
            _pc = pc;
            _closeToken = closeToken;
            
            charAnim.runtimeAnimatorController = entry.AnimCon;
            MessageAsync(entry.Message).Forget();
        }

        private async UniTask MessageAsync(MessageGroup[] msgArr)
        {
            foreach (MessageGroup msg in msgArr)
            {
                _crtMessage = msg.Message;
                
                characterName.SetText(msg.Name);
                
                if (msg.AnimHash.HashValue != AnimHashSO.NullHash)
                    charAnim.Play(msg.AnimHash.HashValue);
                
                messageTmp.ShowText("* " + msg.Message);
                
                msg.Event?.Invoke();
                
                await UniTask.WaitUntil(() => _moveNext);
                
                _moveNext = false;
                messageTmp.TextAnimator.SetText(string.Empty);
            }
            _textTween?.Kill();
            _closeToken?.ExecuteSystem();
        }

        public void MoveNext()
        {
            if (messageTmp.isShowingText)
            {
                messageTmp.StopShowingText();
                messageTmp.TextAnimator.SetText("* " + _crtMessage);
                HandleResizeTmpMesh();
                UIUtils.AdjustmentUI(_parentRect, _pc.ProgramRect.Rect);
                return;
            }

            _moveNext = true;
        }
    }
}