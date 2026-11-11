using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.System
{
    public class ProgramEnable : ProgramSystemBase
    {
        public override void ExecuteSystem()
        {
            base.ExecuteSystem();

            Enable();
        }

        private void Enable()
        {
            ProgramCon.ProgramParent.transform.SetAsLastSibling();
            ProgramCon.ProgramBar.OnEnter?.Invoke();
            ProgramCon.OnEnter?.Invoke();
        }
    }
}