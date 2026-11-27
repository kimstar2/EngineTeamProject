using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message.MessageEventReceiver
{
    public class MessageEvt : MessageEventReceiver<VoidEvt>
    {
        protected override VoidEvt ReturnInvokeType(ProgramController pc)
        {
            return new VoidEvt();
        }
    }
}