using System;
using System.Collections.Generic;
using _TevLib.CoreLib.EventSystem;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.Error;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;
using UnityEngine.Events;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func
{
    public class FuncFilePassword : ProgramFunc
    {
        [SerializeField] private MonoInputField inputField;
        [SerializeField] private UnityEvent onFailEvent;
        private FilePasswordEntry _filePasswordEntry;
        private ProgramSystemBase _closeToken;
        private ProgramSystemBase _enableToken;
        
        public void SetPasswordEntry(FilePasswordEntry entry, IReadOnlyDictionary<ProgramState, ProgramSystemBase> systemDict)
        {
            _filePasswordEntry = entry;

            if (!systemDict.TryGetValue(ProgramState.Closed, out _closeToken))
                return;
            
            if (!systemDict.TryGetValue(ProgramState.Enabled, out _enableToken))
                return;
            
            _filePasswordEntry.OnEnable += HandleEnable;
            _closeToken.OnExecute += HandleClose;
            inputField.InputField.onEndEdit.AddListener(HandleCheckPassword);
        }

        private void HandleCheckPassword(string password)
        {
            if (password == _filePasswordEntry.Password)
            {
                _filePasswordEntry.Open();
                inputField.InputField.onEndEdit.RemoveListener(HandleCheckPassword);
                _closeToken.ExecuteSystem();
            }
            else
            {
                onFailEvent?.Invoke();
            }
        }

        private void HandleEnable()
        {
            _enableToken.ExecuteSystem();
            Debug.Log("enable");
        }
        
        private void HandleClose(ProgramSystemBase _)
        {
            _filePasswordEntry.OnEnable -= HandleEnable;
            _closeToken.OnExecute -= HandleClose;
            _filePasswordEntry.Close();
        }
    }
}