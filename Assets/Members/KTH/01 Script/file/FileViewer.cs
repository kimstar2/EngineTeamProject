using UnityEngine;

public class FileViewer : MonoBehaviour
{
    [SerializeField] private Transform _fileContent;
    [SerializeField] private FileOpen _filePrefab;
    [SerializeField] private PasswordWindow _passwordWindow;

    [SerializeField] private GameObject _viewerPanel;

    public void OpenFile(FileData file)
    {
        if (file.InsideFolder == null)
        {
            return;
        }

        _viewerPanel.SetActive(true);
        ShowFolder(file.InsideFolder);
    }

    private void ShowFolder(FolderData folder)
    {
        foreach (Transform child in _fileContent)
        {
            Destroy(child.gameObject);
        }

        foreach (FileData file in folder.Files)
        {
            FileOpen fileOpen = Instantiate(_filePrefab, _fileContent);
            fileOpen.SetData(file, selectedFile => SelectFile(selectedFile, fileOpen));
        }
    }

    public void SelectFile(FileData file, FileOpen fileOpen)
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

    private void OpenUnlockedFile(FileData file)
    {
        OpenFile(file);
    }

    public void CloseViewer()
    {
        _viewerPanel.SetActive(false);
    }
}