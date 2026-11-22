using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Entry;

namespace Members.KJY.Scripts.Core.Services
{
    public interface IProgramManager
    {
        void MakeChain(ProgramInfoSO programData, ProgramEntry entry);
    }
}