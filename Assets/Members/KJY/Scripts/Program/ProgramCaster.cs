using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.Func;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Members.KJY.Scripts.Program
{

    public static class ProgramCaster
    {
        public static void CastAndSetData(ProgramFunc func, ProgramEntry entry)
        {
            switch (entry.BaseProgram)
            {
                case ProgramType.File:
                    break;
                case ProgramType.Internet:
                    break;
                case ProgramType.RecycleBin:
                    break;
                case ProgramType.Notepad:
                    ((FuncNotePad)func).SetNotePadData((NotepadEntry)entry);
                    break;
                case ProgramType.Explore:
                    break;
            }
        }
    }
}
