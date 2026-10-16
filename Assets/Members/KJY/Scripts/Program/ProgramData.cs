using Members.KJY.Scripts.Program.TaskBar;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program
{
    public class ProgramData
    {
        public GameObject GameObject {get; private set;}
        public MonoRect ProgramRect {get; private set;}
        public ProgramInfo ProgramInfo {get; private set;}
        public ProgramBar ProgramBar {get; private set;}
        
        public ProgramData(MonoRect programRect , GameObject gameObject, ProgramInfo programInfo , ProgramBar programBar)
        {
            ProgramRect = programRect;
            GameObject = gameObject;
            ProgramInfo = programInfo;
            ProgramBar = programBar;
        }
    }
}