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
        [SerializeField] private MonoImage ProgramBar;
        [SerializeField] private MonoImage ProgramBarBackground;
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
            ProgramBar.SetImage(data.ProgramConBar);
            ProgramBar.SetColor(data.ProgramConBarColor);
            
            ProgramBarBackground.SetImage(data.ProgramConBarBg);
            ProgramBarBackground.SetColor(data.BarBgColor);
        }
    }
}