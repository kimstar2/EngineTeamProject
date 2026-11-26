using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program.ApplicationSystem
{
    public class ApplicationDataBinder : MonoModule
    {
        [SerializeField] private MonoImage programIcon;
        [SerializeField] private MonoTMP_UGUI programName;
        
        public void Init(ProgramInfoSO programInfo, ProgramEntry entry)
        {
            programIcon.SetImage(programInfo.ProgramIcon);
            programName.SetText(entry.GetProgramName(programInfo));
        }
    }
}