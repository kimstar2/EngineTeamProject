using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Febucci.UI;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class FuncMessage : ProgramFunc
    {
        [SerializeField] private MonoImage characterIcon;
        [SerializeField] private MonoTMP_UGUI characterName;
        [SerializeField] private TypewriterByCharacter messageTmp;
        private ProgramSystemBase _closeToken;
        private string _crtMessage;
        private bool _moveNext;
        
        public void SetData(MessageEntry entry, ProgramSystemBase closeToken)
        {
            _closeToken = closeToken;
            
            characterIcon.SetImage(entry.PcData.CharacterSprite);
            characterName.SetText(entry.PcData.CharacterName);
            MessageAsync(entry.Message).Forget();
        }

        private async UniTask MessageAsync(string[] msgArr)
        {
            foreach (string msg in msgArr)
            {
                _crtMessage = msg;
                messageTmp.ShowText("* " + _crtMessage);
                
                await UniTask.WaitUntil(() => _moveNext);
                _moveNext = false;
                messageTmp.TextAnimator.SetText(string.Empty);
            }
            _closeToken?.ExecuteSystem();
        }

        public void MoveNext()
        {
            if (messageTmp.isShowingText)
            {
                messageTmp.TextAnimator.SetText("* " + _crtMessage);
                return;
            }

            _moveNext = true;
        }
    }
}