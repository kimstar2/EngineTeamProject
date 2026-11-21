
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Members.KJY.Scripts.Program.Entry;

public class PasswordWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;

    private FileEntry _currentFile;
    private Action<FileEntry> _onSuccess;

    private void Awake()
    {
        _confirmButton.onClick.AddListener(CheckPassword);
        _cancelButton.onClick.AddListener(Close);
    }

    public void Open(FileEntry fileEntry, Action<FileEntry> onSuccess)
    {
        _currentFile = fileEntry;
        _onSuccess = onSuccess;

        _title.SetText(fileEntry.ProgramName + " 비밀번호 입력");
        _passwordInput.text = "";
        _errorText.SetText("");

        gameObject.SetActive(true);
        _passwordInput.ActivateInputField();
    }

    private void CheckPassword()
    {
        if (_currentFile.CheckPassword(_passwordInput.text))
        {
            _currentFile.Unlock();
            _onSuccess?.Invoke(_currentFile);
            Close();
        }
        else
        {
            _errorText.SetText("비밀번호가 틀렸습니다.");
        }
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}
