using System;
using _TevLib.ServiceLocatorSystem;
using Cysharp.Threading.Tasks;
using Members.KJY.Scripts.Core.Services;
using Members.KJY.Scripts.Program.Entry;
using Unity.VisualScripting;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FileOpener : AbstractOpener
    {
        [SerializeField] private ProgramInfoSO checkPassword;
        private FileEntry _fileData;
        private IProgramManager _prgManager;
        private FilePasswordEntry _filePassword;
        
        public event Action<FileEntry> OnInFile;

        public void Init(FileEntry fileData)
        {
            _fileData = fileData;
            _prgManager =ServiceLocator.GetService<IProgramManager>();
        }

        public void Init(FileEntry fileData, PasswordWindow n) { } // 지울거
        
        public override void OpenProgram()
        {
            if (_fileData == null) return;

            if (_filePassword != null)
            {
                _filePassword.Enable();
                return;
            }
            
            if (_fileData.IsLocked)
            {
                _filePassword = ScriptableObject.CreateInstance<FilePasswordEntry>();
                _filePassword.Init(_fileData.Password,_fileData.RenderOrder);
                
                _filePassword.OnPasswordSuccess += HandleOpen;
                _filePassword.OnClose += HandleClose;
                
                _prgManager.MakeChain(checkPassword,_filePassword);
                return;
            }

            OnInFile?.Invoke(_fileData);
            _fileData.InFile?.Invoke();
        }
        
        private void HandleClose()
        {
            _filePassword.OnPasswordSuccess -= HandleOpen;
            _filePassword.OnClose -= HandleClose;
            _filePassword = null;
        }

        private void HandleOpen()
        {
            HandleClose();
            OnInFile?.Invoke(_fileData);
            _fileData.InFile?.Invoke();
        }
    }
}
