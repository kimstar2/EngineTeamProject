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
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Assets.Members.KJY.Scripts.Program
{

    public static class ProgramCaster
    {
        public static void CastAndSetData(ProgramFunc func, ProgramEntry entry, ProgramController pc)
        {
            switch (entry.BaseProgram)
            {
                case ProgramType.Internet:
                {
                    var t = Get<FuncInternet,InternetEntry>(func,entry);
                    t.func.SearchSystem.SetInternetData(t.entry);
                    break;
                }
                case ProgramType.Notepad:
                {
                    var t = Get<FuncNotePad,NotepadEntry>(func,entry);
                    t.func.SetNotePadEntry(t.entry);
                    break;
                    
                }
                case ProgramType.AdventureOS:
                {
                    var t = Get<FuncAdventureOS,AdventureOSEntry>(func,entry);
                    t.func.FuncMapWindow.InitManager();
                    t.func.FuncMapWindow.MakeWorldMap(t.entry.FirstMap, pc);
                    break;
                }
                case ProgramType.FilePassword:
                {
                    var t = Get<FuncFilePassword,FilePasswordEntry>(func,entry);
                    t.func.SetPasswordEntry(t.entry, pc.ProgramReadonlyDict);
                    break;
                }
                case ProgramType.Error:
                {
                    var t = Get<FuncError,ErrorEntry>(func,entry);
                    pc.ProgramReadonlyDict.TryGetValue(ProgramState.Closed, out var closeToken);
                    t.func.SetErrorEntry(t.entry, closeToken);
                    break;
                }
                case ProgramType.WorldMap:
                {
                    var t = Get<FuncMapWindow,MapEntry>(func,entry);
                    t.func.InitManager();
                    t.func.MakeWorldMap(t.entry,pc);
                    break;
                }
                case ProgramType.Message:
                {
                    var t = Get<FuncMessage,MessageEntry>(func,entry);
                    pc.ProgramReadonlyDict.TryGetValue(ProgramState.Closed, out var closeToken);
                    t.func.SetData(t.entry,pc,closeToken);
                    break;
                }
                // 필요한 애들은 추가해서 쓰기
            }
        }

        private static (T func,T1 entry) Get<T,T1>(ProgramFunc f, ProgramEntry e) where T : ProgramFunc where T1 : ProgramEntry
        {
            return (f as T, e as T1); 
        }    
    }
}
