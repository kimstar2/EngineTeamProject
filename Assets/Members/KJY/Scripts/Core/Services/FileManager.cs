using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Check;
using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Func.FuncFileExplorer;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Core.Services
{
    public class FileManager : MonoBehaviour , IFileManager , IRequiredPooling
    {
        [Header("Base Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private ProgramInfoSO fileInfoBase;
        [SerializeField] private FuncFileExplorer fileExplorerBase;
        [SerializeField] private FileSO[] defaultFiles;
        
        public void MakeDefaults()
        {
            foreach (FileSO f in defaultFiles)
                MakeFile(f);
        }
        
        public void MakeFile(FileSO fileData)
        {
            FuncFileExplorer explorer = Instantiate(fileExplorerBase);
            explorer.SetFileData(fileData);

            ProgramInfoSO realInfo = Instantiate(fileInfoBase);
            realInfo.AddFunc(explorer);
                
            ServiceLocator.GetService<IApplicationManager>().Make(realInfo);
        }
    }
}