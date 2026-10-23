using TMPro;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func
{
    [CreateAssetMenu(fileName = "NotePad Data", menuName = "KJY/Program/Func/NotePad Data", order = 0)]
    public class NotePadDataSO : ScriptableObject
    {
        [field:SerializeField] public TextWrappingModes TextWrapMode {get; private set;}
        
        [field:TextArea]
        [field:SerializeField] public string DefaultContent {get; private set;}
    }
}