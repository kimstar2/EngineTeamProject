using UnityEngine;

namespace Members.KJY.Scripts.Program.ApplicationSystem
{
    public abstract class ApplicationSO : ScriptableObject
    {
        [field: SerializeField] public string Name {get; private set;}
        [field: SerializeField] public string FilenameEx {get; private set;}
    }
}