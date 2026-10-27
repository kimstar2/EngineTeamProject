using _TevLib.ModuleSystem;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program
{
    public class ProgramControllerBarDataBinder : MonoModule
    {
        [Header("Bind Setting")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private MonoImage programBar;
        [SerializeField] private MonoImage programBarBackground;
        [SerializeField] private MonoImage iconImage;
        [SerializeField] private MonoTMP_UGUI nameTMP;
        public ProgramInfoSO CurrentProgramData {get; private set;}
        

        public void SetData(ProgramInfoSO data)
        {
            CurrentProgramData = data;
            
            iconImage.SetImage(data.ProgramIcon);
            iconImage.SetColor(data.IconColor);
            nameTMP.SetText(data.ProgramName);
            nameTMP.SetColor(data.ProgramConBarNameColor);
            programBar.SetImage(data.ProgramConBar);
            programBar.SetColor(data.ProgramConBarColor);
            
            programBarBackground.SetImage(data.ProgramConBarBg);
            programBarBackground.SetColor(data.BarBgColor);
        }
    }
}