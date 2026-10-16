using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program
{
    [CreateAssetMenu(fileName = "Program Bar data", menuName = "KJY/TaskBar/Program Bar data", order = 0)]
    public class ProgramInfo : ScriptableObject
    {
        [field: SerializeField] public string ProgramName { get; private set; }
        [field: SerializeField] public Sprite ProgramSprite { get; private set; }
        
        [field:Header("Open Setting")]
        [field:HorizontalLine(color: EColor.Gray)]
        [field:SerializeField] public Vector2 StartPosition { get; private set; }
        [field:SerializeField] public Vector2 StartSize { get; private set; }
    }
}