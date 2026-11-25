using Cysharp.Threading.Tasks;
using Members.KJY.Scripts.Program.Entry;
using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class MessageEntry : ProgramEntry
    {
        public AnimatorController AnimCon { get; private set; }
        public MessageGroup[] Message { get; private set; }
        
        
        public void SetMessageData(AnimatorController pcData, MessageGroup[] msgArr)
        {
            BaseProgram = ProgramType.Message;
            AnimCon = pcData;
            Message = msgArr;
        }
    }
}