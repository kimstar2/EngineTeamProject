using System.Collections.Generic;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    [CreateAssetMenu(fileName = "File Data", menuName = "KJY/Program/Func/File Data", order = 0)]
    public class FileSO : ScriptableObject
    {
        [field: SerializeField] public string FileName {get; private set;}
        [field:SerializeField] public List<FileSO> FileList { get; private set; }
        [field:SerializeField] public List<ProgramInfoSO> ProgramList { get; private set; }
    }
}