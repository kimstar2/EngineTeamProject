using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FileDataBinder : MonoModule
    {
        [SerializeField] private MonoTMP_UGUI fileName;
        
        public void Init(FileEntry fileEntry)
        {
            fileName.SetText(fileEntry.ProgramName);
        }
    }
}