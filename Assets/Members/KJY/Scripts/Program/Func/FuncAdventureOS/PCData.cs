using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS
{
    [CreateAssetMenu(fileName = "PC Data", menuName = "KJY/Adventure OS/PCData", order = 0)]
    public class PCData : ScriptableObject
    {
        [field: SerializeField] public AnimatorController AnimCon {get; private set;}
    }
}