using Members.KJY.Scripts.Program.System;

namespace Members.KJY.Scripts.Program
{
    public interface IProgramInit
    {
        ProgramData ProgramData { get; }
        void ProgramInit(ProgramData programData);
    }
}