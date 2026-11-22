using System;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Program.ApplicationSystem;
using Members.KJY.Scripts.Program.Entry;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FileCell : ModuleOwner
    {
        public FileDataBinder DataBinder {get; private set;}
        public FileOpener Opener {get; private set;}
        public FileEntry File {get; private set;}

        protected override void InitializeModules()
        {
            base.InitializeModules();
            DataBinder = GetModule<FileDataBinder>();
            Opener = GetModule<FileOpener>();
        }

        public void Init(FileEntry fileData)
        {
            DataBinder.Init(fileData);
            Opener.Init(fileData);
            
            File = fileData;
        }
    }
}