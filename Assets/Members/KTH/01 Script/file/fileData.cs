using UnityEngine;

[CreateAssetMenu(fileName = "fileData", menuName = "KTH/FileData")]
public class FileData : ScriptableObject
{
    public string FileName;
    public string UnlockedFileName;
    public string Password;
    public bool IsLocked;


    public FolderData InsideFolder;
}
