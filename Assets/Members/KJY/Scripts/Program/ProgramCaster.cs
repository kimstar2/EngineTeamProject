using Assets.Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.Func;
using Members.KJY.Scripts.Program.Func.FuncInternet;
using System;
using System.Collections.Generic;
using System.Text;
using Members.JJW._01.Code;
using Members.KJY.Scripts.Program.Func.FuncAdventureOS;
using Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message;
using Members.KJY.Scripts.Program.Func.FuncFileExplorer;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.World;
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
                case ProgramType.AdventureOS:
                    Debug.Log("Im ironMan");
                    ((FuncAdventureOS)func).MapWindow.MakeWorldMap(((AdventureOSEntry)entry).FirstMap);
                    break;
                
                case ProgramType.FilePassword:
                    ((FuncFilePassword)func).SetPasswordEntry((FilePasswordEntry)entry, pc.ProgramReadonlyDict);
                    break;
                case ProgramType.Error:
                {
                    if (pc.ProgramReadonlyDict.TryGetValue(ProgramState.Closed, out var closeToken))
                        ((FuncError)func).SetErrorEntry((ErrorEntry)entry, closeToken);
                    break;
                }
                case ProgramType.Message:
                {
                    if (pc.ProgramReadonlyDict.TryGetValue(ProgramState.Closed, out var closeToken))
                        ((FuncMessage)func).SetData((MessageEntry)entry,closeToken);
                    break;
                }
                case ProgramType.Download:
                    ((FuncDownload)func).SetDownloadEntry((FileEntry)entry, pc.ProgramReadonlyDict);
                    break;
            }
        }
    }
}
