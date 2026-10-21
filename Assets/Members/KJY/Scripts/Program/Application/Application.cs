using System;
using _TevLib.ModuleSystem;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Application
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

        private void Start()
        {
            DataBinder.Init(ProgramInfo);
            Opener.Init(ProgramInfo);
        }

        public void Init(ProgramInfoSO info)
        {
            ProgramInfo = info;
        }
    }
}