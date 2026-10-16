using UnityEngine;

namespace Members.KJY.Scripts.Program.System
{
    public class ProgramNothing : ProgramSystemBase
    {
        public override void ExecuteSystem()
        {
            base.ExecuteSystem();
            EndSystem();
        }
    }
}