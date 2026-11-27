using Members.KJY.Scripts.Program.Caster;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.Func.FuncFileExplorer;
using UnityEngine;

namespace Members.KJY.Scripts.Program.FuncDelegate
{
    public class FileExplorerDelegator : FileExplorerCaster
    {
        public void AddEntry(FileEntry entry)
        {
            FileExplorer.MakeFile(entry);
        }  
        
        public void AddEntry(ProgramEntry entry)
        {
            FileExplorer.MakeApp(entry);
        }
    }
}