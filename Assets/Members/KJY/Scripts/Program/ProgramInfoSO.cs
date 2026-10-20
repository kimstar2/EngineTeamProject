using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

namespace Members.KJY.Scripts.Program
{
    [CreateAssetMenu(fileName = "Program Bar data", menuName = "KJY/Program/Program Bar data", order = 0)]
    public class ProgramInfoSO : ScriptableObject
    {
        [field:Header("Program Bar Setting")]
        [field:HorizontalLine(color: EColor.Gray)]
        [field: SerializeField] public Sprite ProgramIcon { get; private set; }
        [field: SerializeField] public Color IconColor { get; private set; } = Color.white;
        
        [field:SerializeField] public Sprite ProgramConBar { get; private set; }
        [field:SerializeField] public Color ProgramConBarColor { get; private set; } = Color.white;
        
        [field: SerializeField] public Sprite ProgramConBarBg { get; private set; }
        [field: SerializeField] public Color BarBgColor { get; private set; } = Color.white;
        
        [field: SerializeField] public string ProgramName { get; private set; }
        [field: SerializeField] public Color ProgramConBarNameColor { get; private set; } = Color.white;
        [field: SerializeField] public Color ProgramBarNameColor { get; private set; } = Color.white;
        
        [field:Header("Program Setting")]
        [field:HorizontalLine(color: EColor.Gray)]
        [field: SerializeField] public Sprite ProgramConBg { get; private set; }
        [field: SerializeField] public Color ProgramConBgColor { get; private set; } = Color.white;
        
        [field:Header("Open Setting")]
        [field:HorizontalLine(color: EColor.Gray)]
        [field:SerializeField] public Vector2 StartPosition { get; private set; }
        [field:SerializeField] public Vector2 StartSize { get; private set; }
        [field:SerializeField] public List<ProgramSystems> ProgramSystems { get; private set; }
    }
}