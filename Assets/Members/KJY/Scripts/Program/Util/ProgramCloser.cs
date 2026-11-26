using UnityEngine;

namespace Members.KJY.Scripts.Program.Util
{
    public class ProgramCloser : MonoBehaviour
    {
        public void CloseProgram(ProgramController pc)
        {
            pc.SystemStateMachine.ChangeState(SystemInvoker.ProgramManager,ProgramState.Closed);
        }
    }
}