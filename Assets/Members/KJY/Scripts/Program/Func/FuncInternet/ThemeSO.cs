using NaughtyAttributes;
using UnityEngine;

namespace Assets.Members.KJY.Scripts.Program.Func.FuncInternet
{
    [CreateAssetMenu(fileName = "Theme data", menuName = "KJY/Program/Func/Theme data", order = 0)]
    public class ThemeSO : ScriptableObject
    {
        [field: Header("Screen Setting")]
        [field: HorizontalLine(color: EColor.Gray)]
        [field: SerializeField] public Color TitleColor {get; private set;}
        [field: SerializeField] public Color BgColor {get; private set;}


        [field:Header("SearchBar Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field: SerializeField] public Color SearchBarOutlineColor{get; private set;}
        [field: SerializeField] public Color SearchBarColor {get; private set;}
        
    }
}