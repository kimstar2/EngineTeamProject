
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Members.KJY.Scripts.Program.Entry;

public class DownloadItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _fileNameText;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private Slider _progressBar;

    public bool IsCompleted { get; private set; }
    public FileEntry FileData { get; private set; }

    public void SetData(FileEntry fileData)
    {
        FileData = fileData;
        _fileNameText.text = fileData.ProgramName;
        _progressBar.value = 0f;
        _progressText.gameObject.SetActive(true);
        _progressText.text = "0%";
        IsCompleted = false;
    }

    public void SetProgress(float progress)
    {
        if (IsCompleted) return;

        progress = Mathf.Clamp01(progress);
        _progressBar.value = progress;

        if (progress >= 1f)
        {
            IsCompleted = true;
            _progressText.text = "완료";
        }
        else
        {
            _progressText.text = $"{Mathf.RoundToInt(progress * 100f)}%";
        }
    }
}
