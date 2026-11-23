using UnityEngine;

namespace Members.KJY.Scripts.Program.Entry
{
    public abstract class ProgramEntry : ScriptableObject
    {
        [field: SerializeField] public ProgramType BaseProgram { get; protected set; }
        [field: SerializeField] public string ProgramName { get; protected set; }
        [field: SerializeField] public bool AwakeOpen { get; protected set; }
    }
}