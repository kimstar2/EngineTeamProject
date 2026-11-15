using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PasswordWindow : MonoBehaviour
{
    [SerializeField] private TMP_InputField _passwordInput;
    [SerializeField] private TMP_Text _errorText;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _cancelButton;

    private FileData _currentFile;
    private Action<FileData> _onSuccess;

    private void Awake()
    {
        _confirmButton.onClick.AddListener(CheckPassword);
        _cancelButton.onClick.AddListener(Close);
    }

    public void Open(FileData fileData, Action<FileData> onSuccess)
    {
        _currentFile = fileData;
        _onSuccess = onSuccess;

        _title.text = fileData.FileName + " 비밀번호 입력";
        _passwordInput.text = "";
        _errorText.text = "";

        gameObject.SetActive(true);
        _passwordInput.ActivateInputField();
    }

    private void CheckPassword()
    {
        if (_passwordInput.text == _currentFile.Password)
        {
            _currentFile.IsLocked = false;
            _onSuccess?.Invoke(_currentFile);
            Close();
        }
        else
        {
            _errorText.text = "비밀번호가 틀렸습니다.";
        }
    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}