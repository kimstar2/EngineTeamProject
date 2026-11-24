
using Members.KJY.Scripts.Program.Entry;
using UnityEngine;

public class DownloadTest : MonoBehaviour
{
    [SerializeField] private DownloadSystem _downloadWindowPrefab;
    [SerializeField] private Transform _canvas;
    [SerializeField] private Transform _desktopContent;
    [SerializeField] private FileEntry _testFolder;

    private DownloadSystem _currentWindow;

    public void StartDownload()
    {
        if (_currentWindow != null) return;

        _currentWindow = Instantiate(_downloadWindowPrefab, _canvas);
        _currentWindow.SetDesktopContent(_desktopContent);
        _currentWindow.SetFolder(_testFolder);
    }
}
