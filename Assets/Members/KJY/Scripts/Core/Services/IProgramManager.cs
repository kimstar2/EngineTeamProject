using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Entry;

namespace Members.KJY.Scripts.Core.Services
{
    public interface IProgramManager
    {
        ProgramController MakeChain(ProgramInfoSO programData, ProgramEntry entry);
        public bool GetEventProgram(ProgramEventSO eventSo, out ProgramController[] program);
    }
}