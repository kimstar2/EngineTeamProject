using System.Collections.Generic;
using UnityEngine;

namespace Members.KJY.Scripts.Program
{
    [CreateAssetMenu(fileName = "Program List", menuName = "KJY/Program/Program List", order = 0)]
    public class ProgramListSO : ScriptableObject
    {
        [field: SerializeField] public List<Program> ProgramList { get; private set; }
    }
}