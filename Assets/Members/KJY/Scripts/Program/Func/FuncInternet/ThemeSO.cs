using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncInternet
{
    [CreateAssetMenu(fileName = "Theme data", menuName = "KJY/Program/Func/Theme data", order = 0)]
    public class ThemeSO : ScriptableObject
    {
        [field: Header("Screen Setting")]
        [field: HorizontalLine(color: EColor.Gray)]
        [field: SerializeField] public Color MainColor { get; private set; } = Color.white;
        [field: SerializeField] public Color SubColor1 {get; private set; } = Color.white;
        [field: SerializeField] public Color SubColor2 {get; private set; } = Color.white;
    }
}