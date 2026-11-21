using System;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Entry
{
    public class FilePasswordEntry : ProgramEntry
    {
        public string Password { get; private set; }
        public event Action OnPasswordSuccess;
        public event Action OnClose;
        public event Action OnEnable;

        public void Init(string pw)
        {
            Password = pw;
            BaseProgram = ProgramType.FilePassword;
        }
        
        public void Open()
        {
            OnPasswordSuccess?.Invoke();
        }
        
        public void Close()
        {
            OnClose?.Invoke();
        }

        public void Enable()
        {
            OnEnable?.Invoke();
        }
    }
}