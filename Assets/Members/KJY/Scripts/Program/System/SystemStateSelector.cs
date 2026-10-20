using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.System
{
    public class SystemStateSelector : MonoBehaviour
    {
        [SerializeField] private ProgramController _programController;
        [SerializeField] private ProgramState programState;

        public void Select()
        {
            _programController.ChangeState(SystemInvoker.ProgramController,programState);
        }
        
        #if UNITY_EDITOR

        private void OnValidate()
        {
            gameObject.name = $"ChangeStateTo_{programState}";
        }

#endif
    }
}