using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message.MessageEventReceiver
{
    public class TrmMessageEvent : MessageEventReceiver<Transform>
    {
        protected override Transform ReturnInvokeType(ProgramController pc) => pc.transform;
    }
}