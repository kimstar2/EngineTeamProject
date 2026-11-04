using System;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Program.ApplicationSystem;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FileCell : ModuleOwner
    {
        public FileSO FileData {get; private set;}
        public ApplicationDataBinder DataBinder {get; private set;}
        
        public event Action<FileSO> OnInFile;
        
        public void Init(FileSO fileData)
        {
            FileData = fileData;
            DataBinder = GetModule<ApplicationDataBinder>();
        }
    }
}