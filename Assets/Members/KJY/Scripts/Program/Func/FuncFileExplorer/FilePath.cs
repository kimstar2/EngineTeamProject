using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FilePath : MonoBehaviour
    {
        [SerializeField] private bool canClick;
        [SerializeField] private MonoTMP_UGUI path;

        public void SetPath(string t)
        {
            path.SetText(t);
        }
    }
}