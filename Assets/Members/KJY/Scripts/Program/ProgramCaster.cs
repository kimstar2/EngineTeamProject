using Assets.Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.Func;
using Members.KJY.Scripts.Program.Func.FuncInternet;
using System;
using System.Collections.Generic;
using System.Text;
using Members.KJY.Scripts.Program.Func.FuncFileExplorer;
using Members.KJY.Scripts.Program.System;
using UnityEngine;

namespace Assets.Members.KJY.Scripts.Program
{

    public static class ProgramCaster
    {
        public static void CastAndSetData(ProgramFunc func, ProgramEntry entry, ProgramController pc)
        {
            switch (entry.BaseProgram)
            {
                case ProgramType.File:
                    break;
                case ProgramType.Internet:
                    ((FuncInternet)func).SearchSystem.SetInternetData((InternetEntry)entry);
                    break;
                case ProgramType.RecycleBin:
                    break;
                case ProgramType.Notepad:
                    ((FuncNotePad)func).SetNotePadEntry((NotepadEntry)entry);
                    break;
                case ProgramType.Explore:
                    break;
                case ProgramType.FilePassword:
                    ((FuncFilePassword)func).SetPasswordEntry((FilePasswordEntry)entry, pc.ProgramReadonlyDict);
                    Debug.Log("만들다");
                    break;
                case ProgramType.Error:
                {
                    if (pc.ProgramReadonlyDict.TryGetValue(ProgramState.Closed, out var closeToken))
                        ((FuncError)func).SetErrorEntry((ErrorEntry)entry, closeToken);
                    break;
                }
            }
        }
    }
}
