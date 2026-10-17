using UnityEditor;
using UnityEngine;

namespace Members.KJY.Scripts.Program.System
{
    [CreateAssetMenu(fileName = "System State", menuName = "KJY/Program/System/System State", order = 0)]
    public class SystemStateSO : ScriptableObject
    {
        [field:SerializeField] public ProgramSystemBase Program { get; private set; }
    }
}