using UnityEngine;

namespace Members.KJY.Scripts.Program.Func
{
    [CreateAssetMenu(fileName = "Theme data", menuName = "KJY/Program/Func/Theme data", order = 0)]
    public class ThemeSO : ScriptableObject
    {
        [field: SerializeField] public Color BgColor {get; private set;}
        [field: SerializeField] public Color SearchBarColor {get; private set;}
        [field: SerializeField] public Color TitleColor {get; private set;}
        
    }
}