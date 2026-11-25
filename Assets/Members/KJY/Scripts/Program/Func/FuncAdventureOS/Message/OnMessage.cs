using _TevLib.CoreLib.EventSystem;
using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class OnMessage : GameEvent
    {
        public readonly AnimatorController AnimCon;
        public readonly MessageGroup[] MessageGroup;
        public readonly Vector2 Position;
        
        public OnMessage(AnimatorController animCon,MessageGroup[] msg, Vector2 pos)
        {
            AnimCon = animCon;
            MessageGroup = msg;
            Position = pos;
        }
    }
}