using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message.MessageEventReceiver
{
    public class RectTrmMessageEvent : MessageEventReceiver<RectTransform>
    {
        protected override RectTransform ReturnInvokeType(ProgramController pc) => pc.ProgramRect.Rect;
    }
}