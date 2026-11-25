using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public interface IMessageRaiser
    {
        public void Raise(AnimatorController data, MessageGroup[] message, Vector2 pos);
        public void PlayerRaise(MessageGroup[] message, Vector2 pos);
    }
}