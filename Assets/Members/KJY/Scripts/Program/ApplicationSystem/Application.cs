using System.Collections.Generic;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.Func;
using UnityEngine;

namespace Members.KJY.Scripts.Program.ApplicationSystem
{
    public class Application : ModuleOwner
    {
        public ProgramInfoSO ProgramInfo {get; private set;}
        
        public ApplicationDataBinder DataBinder {get; private set;}
        public ApplicationOpener Opener {get; private set;}
        
        protected override void InitializeModules()
        {
            base.InitializeModules();
            DataBinder = GetModule<ApplicationDataBinder>();
            Opener = GetModule<ApplicationOpener>();
        }

        public void Init(ProgramInfoSO info, ProgramEntry entry)
        {
            ProgramInfo = Instantiate(info);

            DataBinder.Init(ProgramInfo, entry);
            Opener.Init(ProgramInfo,entry);
        }

        public void SetFunc(List<ProgramFunc> funcList)
        {
            ProgramInfo.FuncPrefabs.Clear();
            foreach (ProgramFunc func in funcList)
                ProgramInfo.AddFunc(func);
        }
    }
}