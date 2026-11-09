using System;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FileOpener : AbstractOpener
    {
        public event Action<FileSO> OnInFile;
        private FileSO _fileData;

        public void Init(FileSO fileData)
        {
            _fileData = fileData;
        }
        
        public override void OpenProgram()
        {
            OnInFile?.Invoke(_fileData);
        }
    }
}