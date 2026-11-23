using System;
using _TevLib.CoreLib.EventSystem;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class AdvOS_MessageRaiser : MonoBehaviour , IMessageRaiser
    {
        [SerializeField] private EventChannelSO eventChannel;
        [SerializeField] private PCData playerData;

        public void Raise(PCData data, string[] message, Vector2 pos)
        {
            eventChannel.Raise(new OnMessage(data, message, pos));
        }

        public void PlayerRaise(string[] message, Vector2 pos)
        {
            Raise(playerData, message, pos);
        }
    }
}