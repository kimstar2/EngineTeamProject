using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func
{
    public class FuncInternet : ProgramFunc
    {
        [SerializeField] private MonoImage bg;
        [SerializeField] private MonoImage searchBar;
        [SerializeField] private MonoTMP_UGUI title;
        
        public void ChangeTheme(ThemeSO theme)
        {
            bg.SetColor(theme.BgColor);
            searchBar.SetColor(theme.SearchBarColor);
            title.SetColor(theme.TitleColor);
        }
    }
}