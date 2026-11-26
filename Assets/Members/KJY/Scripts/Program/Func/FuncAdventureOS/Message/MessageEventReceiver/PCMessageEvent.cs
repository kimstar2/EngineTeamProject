using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message.MessageEventReceiver
{
    public class PCMessageEvent : MessageEventReceiver<ProgramController>
    {
        protected override ProgramController ReturnInvokeType(ProgramController pc) => pc;
    }
}