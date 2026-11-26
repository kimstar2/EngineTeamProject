using System;
using System.Collections.Generic;
using _TevLib.CoreLib.EventSystem;
using Members.KJY.Scripts.Player;
using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class OnMessage : GameEvent
    {
        public readonly AnimatorController AnimCon;
        public readonly MessageListSO MessageGroup;
        public readonly Vector2 Position;
        public readonly Dictionary<MessageEventSO,List<Action<ProgramController>>> EventDict;
        
        public OnMessage(AnimatorController animCon,MessageListSO msg, Vector2 pos, Dictionary<MessageEventSO,List<Action<ProgramController>>> eventDict)
        {
            AnimCon = animCon;
            MessageGroup = msg;
            Position = pos;
            EventDict = eventDict;
        }
    }
}