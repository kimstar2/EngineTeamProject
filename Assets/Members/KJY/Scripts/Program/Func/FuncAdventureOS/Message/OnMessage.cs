using _TevLib.CoreLib.EventSystem;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class OnMessage : GameEvent
    {
        public readonly PCData PcData;
        public readonly string[] Message;
        public readonly Vector2 Position;
        
        public OnMessage(PCData pcData, string[] msg, Vector2 pos)
        {
            PcData = pcData;
            Message = msg;
            Position = pos;
        }
    }
}