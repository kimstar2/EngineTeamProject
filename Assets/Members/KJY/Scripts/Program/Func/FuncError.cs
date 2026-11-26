using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func
{
    public class FuncError : ProgramFunc
    {
        [SerializeField] private MonoTMP_UGUI errorTmp;
        private ProgramSystemBase _closeToken;
        
        public void SetErrorEntry(ErrorEntry errorEntry, ProgramSystemBase closeToken)
        {
            _closeToken = closeToken;
            errorTmp.SetText(errorEntry.ErrorMessage);
        }

        public void Ok()
        {
            _closeToken?.ExecuteSystem();
        }
    }
}