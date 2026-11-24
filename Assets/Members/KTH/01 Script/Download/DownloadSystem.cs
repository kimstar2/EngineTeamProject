using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Members.KJY.Scripts.Program.Entry;

public class DownloadSystem : MonoBehaviour
{
    [Header("Download Setting")]
    [SerializeField] private DownloadItem _downloadItemPrefab;
    [SerializeField] private Transform _downloadContent;
    [SerializeField] private float _downloadDuration = 5f;

    [Header("Desktop File Settings")]
    [SerializeField] private FileOpen _filePrefab;

    private Transform _desktopContent;

    private readonly List<DownloadItem> _downloadItems = new();
    private readonly List<float> _progressList = new();

    private bool _isDownloading;

    public void SetDesktopContent(Transform desktopContent)
    {
        _desktopContent = desktopContent;
    }

    public void SetFolder(FileEntry folder)
    {
        if (_isDownloading || folder == null) return;

        if (_downloadItemPrefab == null || _downloadContent == null)
        {
            Debug.LogError("DownloadItemPrefab 또는 DownloadContent가 연결되지 않았습니다.");
            return;
        }

        if (_filePrefab == null || _desktopContent == null)
        {
            Debug.LogError("FilePrefab 또는 DesktopContent가 연결되지 않았습니다.");
            return;
        }

        ClearDownloads();
        _isDownloading = true;

        if (folder.FileList == null || folder.FileList.Count == 0)
        {
            _isDownloading = false;
            StartCoroutine(CloseDownloadWindow());
            return;
        }

        foreach (FileEntry file in folder.FileList)
        {
            if (file != null)
                AddDownload(file);
        }

        if (_downloadItems.Count == 0)
        {
            _isDownloading = false;
            StartCoroutine(CloseDownloadWindow());
        }
    }

    private void AddDownload(FileEntry fileData)
    {
        DownloadItem item = Instantiate(_downloadItemPrefab, _downloadContent);
        item.SetData(fileData);

        _downloadItems.Add(item);
        _progressList.Add(0f);
    }

    private void Update()
    {
        if (!_isDownloading) return;

        for (int i = 0; i < _downloadItems.Count; i++)
        {
            DownloadItem item = _downloadItems[i];

            if (item == null || item.IsCompleted) continue;

            _progressList[i] += Time.deltaTime / Mathf.Max(0.1f, _downloadDuration);
            item.SetProgress(_progressList[i]);

            if (item.IsCompleted)
                CreateDesktopFile(item.FileData);
        }

        bool allCompleted = _downloadItems.TrueForAll(item => item == null || item.IsCompleted);

        if (allCompleted)
        {
            _isDownloading = false;
            StartCoroutine(CloseDownloadWindow());
        }
    }

    private void CreateDesktopFile(FileEntry fileData)
    {
        if (fileData == null) return;

        FileOpen file = Instantiate(_filePrefab, _desktopContent);
        file.SetData(fileData, null);
    }

    private IEnumerator CloseDownloadWindow()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    public void ClearDownloads()
    {
        foreach (DownloadItem item in _downloadItems)
        {
            if (item != null)
                Destroy(item.gameObject);
        }

        _downloadItems.Clear();
        _progressList.Clear();
    }
}
