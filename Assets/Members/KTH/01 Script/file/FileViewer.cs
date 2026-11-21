
using UnityEngine;
using Members.KJY.Scripts.Program.Entry;

public class FileViewer : MonoBehaviour
{
    [SerializeField] private Transform _fileContent;
    [SerializeField] private FileOpen _filePrefab;
    [SerializeField] private PasswordWindow _passwordWindow;
    [SerializeField] private GameObject _viewerPanel;

    public void OpenFile(FileEntry file)
    {
        _viewerPanel.SetActive(true);
        ShowFolder(file);
    }

    private void ShowFolder(FileEntry folder)
    {
        foreach (Transform child in _fileContent)
            Destroy(child.gameObject);

        foreach (FileEntry file in folder.FileList)
        {
            FileOpen fileOpen = Instantiate(_filePrefab, _fileContent);
            fileOpen.SetData(file, selectedFile => SelectFile(selectedFile, fileOpen));
        }
    }

    public void SelectFile(FileEntry file, FileOpen fileOpen)
    {
        if (file.IsLocked)
        {
            _passwordWindow.Open(file, unlockedFile =>
            {
                fileOpen.RefreshName();
                OpenFile(unlockedFile);
            });
            return;
        }

        OpenFile(file);
    }

    public void CloseViewer()
    {
        _viewerPanel.SetActive(false);
    }
    public PasswordWindow GetPasswordWindow()
    {
        return _passwordWindow;
    }
}
