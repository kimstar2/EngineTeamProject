using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program.TaskBar
{
    public class ProgramBarDataBinder : MonoModule
    {
        [Header("Bind Setting")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private MonoImage iconImage;
        [SerializeField] private MonoTMP_UGUI nameTMP;
        public ProgramInfoSO CurrentProgramData {get; private set;}
        

        public void SetData(ProgramInfoSO data,ProgramEntry entry)
        {
            CurrentProgramData = data;
            
            iconImage.SetImage(data.ProgramIcon);
            iconImage.SetColor(data.IconColor);
            nameTMP.SetText(entry != null ? entry.GetProgramName(data) : data.ProgramName);
            nameTMP.SetColor(data.ProgramBarNameColor);
        }
    }
}