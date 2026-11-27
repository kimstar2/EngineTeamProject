using UnityEngine;

namespace Members.KJY.Scripts.Program.Entry
{
    public class ErrorEntry : ProgramEntry
    {
        public string ErrorMessage { get; private set; }

        public void SetErrorMessage(string errorMessage, ProgramRenderOrder renderOrder)
        {
            RenderOrder = renderOrder;
            BaseProgram = ProgramType.Error;
            ErrorMessage = errorMessage;
        }
    }
}