using _TevLib.CoreLib.EventSystem;
using Members.KJY.Scripts.Program.Entry;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Error
{
    public class ErrorSender : MonoBehaviour
    {
        [SerializeField] private EventChannelSO eventChannel;
        
        public void Error(string eM,ProgramRenderOrder programRenderOrder)
        {
            eventChannel.Raise(new OnError(eM,programRenderOrder));
        }
    }
}