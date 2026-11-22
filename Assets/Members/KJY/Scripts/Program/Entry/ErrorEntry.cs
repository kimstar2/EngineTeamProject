using UnityEngine;

namespace Members.KJY.Scripts.Program.Entry
{
    public class ErrorEntry : ProgramEntry
    {
        public string ErrorMessage { get; private set; }

        public void SetErrorMessage(string errorMessage)
        {
            BaseProgram = ProgramType.Error;
            ErrorMessage = errorMessage;
        }
    }
}