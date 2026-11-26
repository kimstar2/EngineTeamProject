using Members.KJY.Scripts.Program.Func;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Entry
{
    [CreateAssetMenu(fileName = "Notepad Entry", menuName = "KJY/Program/Program Entry/Notepad Entry", order = 0)]
    public class NotepadEntry : ProgramEntry
    {
        [field:SerializeField] public TextWrappingModes TextWrapMode {get; private set;}
        
        [field:ResizableTextArea]
        [field:SerializeField] public string DefaultContent {get; private set;}
    }
}