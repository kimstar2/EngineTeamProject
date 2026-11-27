using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Entry;
using UnityEngine;

namespace Members.KJY.Scripts.Core.Services
{
    public interface IProgramAddedManager
    {
        public void ProgramRaise(Sprite sprite, string programName);
        public void ProgramRaise(ProgramInfoSO info, ProgramEntry entry);
    }
}