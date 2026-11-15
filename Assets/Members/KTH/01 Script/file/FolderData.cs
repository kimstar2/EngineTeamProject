using UnityEngine;

[CreateAssetMenu(fileName = "FolderData", menuName = "KTH/FolderData")]
public class FolderData : ScriptableObject
{
    public string FolderName;
    public FileData[] Files;
    public FolderData[] Folders;
}
