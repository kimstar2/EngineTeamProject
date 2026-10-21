using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Application
{
    public class ApplicationDataBinder : MonoModule
    {
        [SerializeField] private MonoImage programIcon;
        [SerializeField] private MonoTMP_UGUI programName;
        
        public void Init(ProgramInfoSO programInfo)
        {
            programIcon.SetImage(programInfo.ProgramIcon);
            programName.SetText(programInfo.ProgramName);
        }
    }
}