using System.Collections.Generic;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Entry
{
    [CreateAssetMenu(fileName = "File Entry", menuName = "KJY/Program/Program Entry/File Entry", order = 0)]
    public class FileEntry : ProgramEntry
    {
        [field:SerializeField] public List<FileEntry> FileList { get; private set; }
        [field:SerializeField] public List<ProgramEntry> ProgramList { get; private set; }
    }
}