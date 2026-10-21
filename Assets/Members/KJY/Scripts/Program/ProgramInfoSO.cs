using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

namespace Members.KJY.Scripts.Program
{
    [CreateAssetMenu(fileName = "Program Bar data", menuName = "KJY/Program/Program Bar data", order = 0)]
    public class ProgramInfoSO : ScriptableObject
    {
        // 여기는 디자인 세팅
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
        
        // 프로그램 세팅
        [field:Header("Open Setting")]
        [field:HorizontalLine(color: EColor.Gray)]
        [field:SerializeField] public Vector2 StartPosition { get; private set; } // 시작 위치
        [field:SerializeField] public Vector2 StartSize { get; private set; } // 시작크기
        [field:SerializeField] public List<ProgramSystems> ProgramSystems { get; private set; } // 기본 시스템
    }
}