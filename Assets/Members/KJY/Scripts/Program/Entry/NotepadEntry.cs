using Members.KJY.Scripts.Program.Func;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Entry
{
    [CreateAssetMenu(fileName = "Notepad Entry", menuName = "KJY/Program/Program Entry/Notepad Entry", order = 0)]
    public class NotepadEntry : ProgramEntry
    {
        [field: SerializeField] public NotePadDataSO NotepadData { get; private set; }
    }
}