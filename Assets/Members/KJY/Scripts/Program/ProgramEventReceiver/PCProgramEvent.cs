using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.ProgramEventReceiver
{
    public class PCProgramEvent : ProgramEventReceiver<ProgramController>
    {
        protected override ProgramController ReturnInvokeType(ProgramController pc) => pc;
    }
}