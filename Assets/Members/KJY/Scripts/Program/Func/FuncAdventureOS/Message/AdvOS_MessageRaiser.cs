using System;
using _TevLib.CoreLib.EventSystem;
using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class AdvOS_MessageRaiser : MonoBehaviour , IMessageRaiser
    {
        [SerializeField] private EventChannelSO eventChannel;
        [SerializeField] private AnimatorController playerData;

        public void Raise(AnimatorController data, MessageGroup[] message, Vector2 pos)
        {
            eventChannel.Raise(new OnMessage(data, message, pos));
        }
        
        public void PlayerRaise(MessageGroup[] message, Vector2 pos)
        {
            Raise(playerData, message, pos);
        }
    }
}