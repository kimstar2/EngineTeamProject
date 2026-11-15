using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FileOpen : MonoBehaviour
{
    [SerializeField] private TMP_Text _fileName;
    [SerializeField] private Button _button;
    [SerializeField] private FileData _fileData;
    [SerializeField] private FileViewer _fileViewer;

    private Action<FileData> _onClick;

    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void Start()
    {
        if (_fileData != null)
        {
            _fileName.text = string.IsNullOrEmpty(_fileData.UnlockedFileName) ? _fileData.FileName : (_fileData.IsLocked ? _fileData.FileName : _fileData.UnlockedFileName);
        }
    }

    public void SetData(FileData data, Action<FileData> onClick)
    {
        _fileData = data;
        _onClick = onClick;
        _fileName.text = data.IsLocked ? data.FileName : (string.IsNullOrEmpty(data.UnlockedFileName) ? data.FileName : data.UnlockedFileName);
    }

    private void OnClick()
    {
        if (_onClick != null)
        {
            _onClick.Invoke(_fileData);
            return;
        }

        if (_fileData != null && _fileViewer != null)
        {
            _fileViewer.SelectFile(_fileData, this);
            return;
        }
    }
    public void RefreshName()
    {
        _fileName.text = _fileData.IsLocked || string.IsNullOrEmpty(_fileData.UnlockedFileName) ? _fileData.FileName : _fileData.UnlockedFileName;
    }
}