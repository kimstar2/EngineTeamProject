using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class FuncMessage : ProgramFunc
    {
        [SerializeField] private MonoImage characterIcon;
        [SerializeField] private MonoTMP_UGUI characterName;
        [SerializeField] private MonoTMP_UGUI messageTmp;
        private ProgramSystemBase _closeToken;
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
                messageTmp.SetText("* " + msg);
                await UniTask.WaitUntil(() => _moveNext);
                _moveNext = false;
                Debug.Log("다음대사");
            }
            _closeToken?.ExecuteSystem();
        }

        public void MoveNext()
        {
            _moveNext = true;
        }
    }
}