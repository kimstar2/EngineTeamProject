using Members.KJY.Scripts.Program.Func;
using Members.KJY.Scripts.Program.Func.FuncFileExplorer;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Caster
{
    public abstract class FileExplorerCaster : FuncCaster
    {
        public FuncFileExplorer FileExplorer { get; private set; }
        public override void Cast(ProgramController pc)
        {
            foreach (ProgramFunc pf in pc.FuncList)
                if (pf is FuncFileExplorer fileExplorer)
                    FileExplorer = fileExplorer;
        }
    }
}