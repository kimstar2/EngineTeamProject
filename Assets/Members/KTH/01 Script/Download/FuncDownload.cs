using FileExplorerFunc = Members.KJY.Scripts.Program.Func.FuncFileExplorer.FuncFileExplorer;
using System.Collections;
using System.Collections.Generic;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Core.Services;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.System;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func
{
    public class FuncDownload : ProgramFunc
    {
        [Header("Download Setting")]
        [SerializeField] private DownloadItem _downloadItemPrefab;
        [SerializeField] private Transform _downloadContent;
        [SerializeField] private float _downloadDuration = 5f;

        [Header("File Setting")]
        [SerializeField] private ProgramInfoSO _fileInfoBase;
        [SerializeField] private FileExplorerFunc _fileExplorerPrefab;

        private readonly List<DownloadItem> _downloadItems = new();
        private readonly List<float> _progressList = new();

        private IApplicationManager _applicationManager;
        private ProgramSystemBase _closeToken;

        private bool _isDownloading;
        private bool _isClosing;

        public override void Init()
        {
            _applicationManager = ServiceLocator.GetService<IApplicationManager>();
        }

        public void SetDownloadEntry(FileEntry folder, IReadOnlyDictionary<ProgramState, ProgramSystemBase> systemDict)
        {
            if (folder == null || _isDownloading) return;

            if (!systemDict.TryGetValue(ProgramState.Closed, out _closeToken)) return;

            _closeToken.OnExecute += HandleClose;

            if (_downloadItemPrefab == null || _downloadContent == null) return;

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
                if (file == null) continue;

                DownloadItem item = Instantiate(_downloadItemPrefab, _downloadContent);
                item.SetData(file);

                _downloadItems.Add(item);
                _progressList.Add(0f);
            }

            if (_downloadItems.Count == 0)
            {
                _isDownloading = false;
                StartCoroutine(CloseDownloadWindow());
            }
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

            if (_downloadItems.TrueForAll(item => item == null || item.IsCompleted))
            {
                _isDownloading = false;
                StartCoroutine(CloseDownloadWindow());
            }
        }

        private void CreateDesktopFile(FileEntry fileData)
        {
            if (fileData == null || _applicationManager == null) return;
            if (_fileInfoBase == null || _fileExplorerPrefab == null) return;

            FileExplorerFunc explorer = Instantiate(_fileExplorerPrefab);
            explorer.SetFileData(fileData);

            ProgramInfoSO realInfo = Instantiate(_fileInfoBase);
            realInfo.SetName(fileData.ProgramName);
            realInfo.AddFunc(explorer);

            _applicationManager.Make(realInfo, fileData);
        }

        private IEnumerator CloseDownloadWindow()
        {
            if (_isClosing) yield break;

            _isClosing = true;

            yield return new WaitForSeconds(1.5f);

            _closeToken?.ExecuteSystem();
        }

        private void HandleClose(ProgramSystemBase system)
        {
            _isDownloading = false;
            _closeToken.OnExecute -= HandleClose;
            ClearDownloads();
        }

        private void ClearDownloads()
        {
            foreach (DownloadItem item in _downloadItems)
            {
                if (item != null)
                    Destroy(item.gameObject);
            }

            _downloadItems.Clear();
            _progressList.Clear();
        }

        private void OnDestroy()
        {
            if (_closeToken != null)
                _closeToken.OnExecute -= HandleClose;
        }
    }
}
