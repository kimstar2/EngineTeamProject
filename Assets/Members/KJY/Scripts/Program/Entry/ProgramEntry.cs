using UnityEngine;

namespace Members.KJY.Scripts.Program.Entry
{
    public abstract class ProgramEntry : ScriptableObject
    {
        [field: SerializeField] public ProgramType BaseProgram { get; private set; }
        [field: SerializeField] public string ProgramName { get; private set; }
    }
}