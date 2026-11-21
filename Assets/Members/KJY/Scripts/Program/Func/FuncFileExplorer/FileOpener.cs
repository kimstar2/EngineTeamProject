using System;
using Members.KJY.Scripts.Program.Entry;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FileOpener : AbstractOpener
    {
        public event Action<FileEntry> OnInFile;

        private FileEntry _fileData;
        private PasswordWindow _passwordWindow;

        public void Init(FileEntry fileData)
        {
            _fileData = fileData;
        }

        public void Init(FileEntry fileData, PasswordWindow passwordWindow)
        {
            _fileData = fileData;
            _passwordWindow = passwordWindow;
        }

        public override void OpenProgram()
        {
            if (_fileData == null) return;

            if (_fileData.IsLocked)
            {
                _passwordWindow.Open(_fileData, OnPasswordSuccess);
                return;
            }

            OnInFile?.Invoke(_fileData);
        }

        private void OnPasswordSuccess(FileEntry fileEntry)
        {
            OnInFile?.Invoke(fileEntry);
        }
    }
}
