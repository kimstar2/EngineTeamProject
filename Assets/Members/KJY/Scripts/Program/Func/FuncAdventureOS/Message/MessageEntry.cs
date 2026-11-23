using Cysharp.Threading.Tasks;
using Members.KJY.Scripts.Program.Entry;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class MessageEntry : ProgramEntry
    {
        public PCData PcData { get; private set; }
        public string[] Message { get; private set; }
        
        
        public void SetMessageData(PCData pcData, string[] msgArr)
        {
            BaseProgram = ProgramType.Message;
            PcData = pcData;
            Message = msgArr;
        }
    }
}