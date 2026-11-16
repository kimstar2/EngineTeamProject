using System;
using Members.KJY.Scripts.Program.Entry;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FileOpener : AbstractOpener
    {
        public event Action<FileEntry> OnInFile;
        private FileEntry _fileData;

        public void Init(FileEntry fileData)
        {
            _fileData = fileData;
        }
        
        public override void OpenProgram()
        {
            OnInFile?.Invoke(_fileData);
        }
    }
}