using Members.KJY.Scripts.Program.Entry;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class MessageEntry : ProgramEntry
    {
        public PCData PcData { get; private set; }
        public string Message { get; private set; }
        
        public void SetMessageData(PCData pcData, string msg)
        {
            BaseProgram = ProgramType.Message;
            PcData = pcData;
            Message = msg;
        }
    }
}