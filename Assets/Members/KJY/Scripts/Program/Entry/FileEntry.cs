using System.Collections.Generic;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Entry
{
    [CreateAssetMenu(fileName = "File Entry", menuName = "KJY/Program/Program Entry/File Entry", order = 0)]
    public class FileEntry : ProgramEntry
    {
        [field:SerializeField] public List<FileEntry> FileList { get; private set; }
        [field:SerializeField] public List<ProgramEntry> ProgramList { get; private set; }

        [field: SerializeField] public bool IsLocked { get; private set; }
        [field: SerializeField] public string Password { get; private set; }
        
        public bool CheckPassword(string input)
        {
            return Password == input;
        }

        public void Unlock()
        {
            IsLocked = false;
        }
    }
}