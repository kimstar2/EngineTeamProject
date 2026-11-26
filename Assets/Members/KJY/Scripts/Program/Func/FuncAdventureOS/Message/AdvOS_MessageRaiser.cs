using System;
using System.Collections.Generic;
using _TevLib.CoreLib.EventSystem;
using Members.KJY.Scripts.Player;
using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class AdvOS_MessageRaiser : MonoBehaviour , IMessageRaiser
    {
        [SerializeField] private EventChannelSO eventChannel;
        [SerializeField] private AnimatorController playerData;
        private readonly Dictionary<MessageEventSO,List<Action<ProgramController>>> _messageDict = new();

        public void Raise(AnimatorController data, MessageListSO message, Vector2 pos)
        {
            eventChannel.Raise(new OnMessage(data, message, pos, _messageDict));
        }

        public void PlayerRaise(MessageListSO message, Vector2 pos)
        {
            Raise(playerData, message, pos);
        }

        public void RegisterEvent(MessageEventSO eventID, Action<ProgramController> action)
        {
            if (_messageDict.TryGetValue(eventID,out List<Action<ProgramController>> list))
                list.Add(action);
            else
                _messageDict.Add(eventID,new List<Action<ProgramController>>{action});
        }

        public void UnRegisterEvent(MessageEventSO eventID, Action<ProgramController> action)
        {
            if (_messageDict.TryGetValue(eventID,out List<Action<ProgramController>> list))
                list.Remove(action);
        }
    }
}