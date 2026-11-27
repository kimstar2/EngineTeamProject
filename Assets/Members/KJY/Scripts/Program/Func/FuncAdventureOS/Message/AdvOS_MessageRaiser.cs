using System;
using System.Collections.Generic;
using _TevLib.CoreLib.EventSystem;
using _TevLib.SoundSystem.Runtime;
using Members.KJY.Scripts.Player;
using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class AdvOS_MessageRaiser : MonoBehaviour , IMessageRaiser
    {
        [SerializeField] private EventChannelSO eventChannel;
        [SerializeField] private AnimatorController playerData;
        [SerializeField] private SoundClipSO[] playerSoundClips;
        private readonly Dictionary<MessageEventSO,List<Action<ProgramController>>> _messageDict = new();

        public void Raise(AnimatorController data, MessageListSO message, Vector2 pos,SoundClipSO[] soundClips)
        {
            eventChannel.Raise(new OnMessage(data, message, pos, _messageDict, soundClips));
        }
        
        public void Raise(AnimatorController data, MessageListSO message, Vector2 pos,List<ProgramSystems> systems,SoundClipSO[] soundClips)
        {
            eventChannel.Raise(new OnMessage(data, message, pos, _messageDict,systems, soundClips));
        }


        public void PlayerRaise(MessageListSO message, Vector2 pos)
        {
            Raise(playerData, message, pos,playerSoundClips);
        }  
        public void PlayerRaise(MessageListSO message, Vector2 pos, List<ProgramSystems> systems)
        {
            Raise(playerData, message, pos,systems,playerSoundClips);
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