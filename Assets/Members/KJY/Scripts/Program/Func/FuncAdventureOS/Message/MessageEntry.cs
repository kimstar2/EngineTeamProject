using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Members.KJY.Scripts.Player;
using Members.KJY.Scripts.Program.Entry;
using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class MessageEntry : ProgramEntry
    {
        public AnimatorController AnimCon { get; private set; }
        public MessageGroup[] Message { get; private set; }
        public Dictionary<MessageEventSO,List<Action<ProgramController>>> EventDict { get; private set; }
        
        
        public void SetMessageData(AnimatorController pcData, MessageListSO msgList,  Dictionary<MessageEventSO,List<Action<ProgramController>>> eventDict)
        {
            RenderOrder = msgList.renderOrder;
            BaseProgram = ProgramType.Message;
            AnimCon = pcData;
            Message = msgList.MessageList;
            EventDict = eventDict;
        }
    }
}