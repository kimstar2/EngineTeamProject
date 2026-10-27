using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func
{
    public class DeleteProgramCell : MonoBehaviour
    {
        [field: SerializeField] public MonoImage IconImage { get; private set; }
        [field: SerializeField] public MonoTMP_UGUI NameText { get; private set; }

        public void Init(ProgramInfoSO info)
        {
            IconImage.SetImage(info.ProgramIcon);
            NameText.SetText(info.ProgramName);
        }
    }
}