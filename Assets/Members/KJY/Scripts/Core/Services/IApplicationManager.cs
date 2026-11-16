using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Entry;

namespace Members.KJY.Scripts.Core.Services
{
    public interface IApplicationManager
    {
        void UnClick();
        void Make(ProgramInfoSO info, ProgramEntry entry);
    }
}