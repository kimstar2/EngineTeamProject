using System.Collections.Generic;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Entry
{
    [CreateAssetMenu(fileName = "File Data", menuName = "KJY/Program/Func/File Data", order = 0)]
    public class FileEntry : ProgramEntry
    {
        [field:SerializeField] public List<FileEntry> FileList { get; private set; }
        [field:SerializeField] public List<ProgramInfoSO> ProgramList { get; private set; }
    }
}