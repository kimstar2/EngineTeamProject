using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public interface IMessageRaiser
    {
        public void Raise(AnimatorController data, MessageListSO message, Vector2 pos);
        public void PlayerRaise(MessageListSO message, Vector2 pos );
        public void RegisterEvent(MessageEventSO eventID, Action<ProgramController> action);
        public void UnRegisterEvent(MessageEventSO eventID, Action<ProgramController> action);

    }
}