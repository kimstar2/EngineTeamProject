using System;
using TMPro;
using UnityEngine;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.Func.FuncFileExplorer;

public class FileOpen : MonoBehaviour
{
    [SerializeField] private TMP_Text _fileName;
    [SerializeField] private FileEntry _fileData;
    [SerializeField] private FileViewer _fileViewer;

    private FileOpener _fileOpener;
    private Action<FileEntry> _onClick;

    private void Awake()
    {
        _fileOpener = GetComponentInChildren<FileOpener>();

        if (_fileData != null)
        {
            _fileName.SetText(_fileData.ProgramName);
            SetupOpener();
        }
    }

    public void SetData(FileEntry data, Action<FileEntry> onClick)
    {
        _fileData = data;
        _onClick = onClick;
        _fileName.SetText(data.ProgramName);
        SetupOpener();
    }

    private void SetupOpener()
    {
        if (_fileOpener == null)
            _fileOpener = GetComponentInChildren<FileOpener>();

        if (_fileOpener == null)
        {
            return;
        }

        _fileOpener.OnInFile -= HandleOpen;
        _fileOpener.Init(_fileData, _fileViewer.GetPasswordWindow());
        _fileOpener.OnInFile += HandleOpen;
    }

    private void HandleOpen(FileEntry file)
    {
        if (_onClick != null)
            _onClick.Invoke(file);
        else
            _fileViewer.SelectFile(file, this);
    }

    public void RefreshName()
    {
        if (_fileData != null)
            _fileName.SetText(_fileData.ProgramName);
    }

    private void OnDestroy()
    {
        if (_fileOpener != null)
            _fileOpener.OnInFile -= HandleOpen;
    }
}
