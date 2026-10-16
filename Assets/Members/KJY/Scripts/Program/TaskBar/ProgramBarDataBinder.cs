using System;
using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program.TaskBar
{
    public class ProgramBarDataBinder : MonoModule
    {
        [HorizontalLine(color:EColor.Gray)]
        [Header("Bind Setting")]
        [SerializeField] private MonoImage iconImage;
        [SerializeField] private MonoTMP_UGUI nameTMP;
        public ProgramInfo CurrentProgramData {get; private set;}
        

        public void SetData(ProgramInfo data)
        {
            CurrentProgramData = data;
            
            iconImage.SetImage(data.ProgramSprite);
            nameTMP.TMP.SetText(data.ProgramName);
        }
    }
}