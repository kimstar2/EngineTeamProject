using System.Collections.Generic;
using Assets.Members.KJY.Scripts.Program.Func;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

namespace Members.KJY.Scripts.Program
{
    [CreateAssetMenu(fileName = "Program Bar data", menuName = "KJY/Program/Program Bar data", order = 0)]
    public class ProgramInfoSO : ScriptableObject
    {
        [field:Header("Program Func Setting")] // 프로그램 기능
        [field:HorizontalLine(color: EColor.Gray)]
        [field:SerializeField] public ProgramFunc[] FuncPrefabs { get; private set; }
        
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

        [field: Header("Size Setting")]
        [field: HorizontalLine(color: EColor.Gray)]
        [field: SerializeField] public Vector2 MinSize { get; private set; } = new(400f, 300f);
        [field:SerializeField] public Vector2 MaxSize { get; private set; } = new(800f, 500f);
    }
}