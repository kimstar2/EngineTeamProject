using Members.KJY.Scripts.Program.TaskBar;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program
{
    public class ProgramData
    {
        public MonoRect ParentRect {get; private set;}
        public MonoRect ProgramRect {get; private set;}
        public GameObject GameObject {get; private set;}
        public ProgramInfoSO ProgramInfo {get; private set;}
        
        public ProgramData(MonoRect parentRect, MonoRect programRect, GameObject gameObject, ProgramInfoSO programInfo)
        {
            ProgramRect = programRect;
            GameObject = gameObject;
            ProgramInfo = programInfo;
            ParentRect = parentRect;
        }
    }
}