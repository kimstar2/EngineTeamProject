using _TevLib.CoreLib.EventSystem;
using Members.KJY.Scripts.Program.Entry;

namespace Members.KJY.Scripts.Program.Error
{
    public class OnError : GameEvent
    {
        public readonly string ErrorMessage;

        public OnError(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}