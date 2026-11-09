using _TevLib.ModuleSystem;

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

        public void Init(ProgramInfoSO info)
        {
            ProgramInfo = info;

            DataBinder.Init(ProgramInfo);
            Opener.Init(ProgramInfo);
        }
    }
}