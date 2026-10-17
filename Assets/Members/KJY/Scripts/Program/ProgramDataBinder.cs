using System;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program
{
    public class ProgramDataBinder : MonoModule
    {
        [field:Header("Bind Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field: SerializeField] public MonoImage ProgramBackground {get; private set;}
        public ProgramInfoSO CurrentProgramData { get; private set; }

        public void SetData(ProgramInfoSO data)
        {
            CurrentProgramData = data;
            
            ProgramBackground.SetImage(data.ProgramConBg);
            ProgramBackground.SetColor(data.ProgramConBgColor);
        }

    }
}