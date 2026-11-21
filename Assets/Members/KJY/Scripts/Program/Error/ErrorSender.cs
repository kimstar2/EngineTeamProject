using _TevLib.CoreLib.EventSystem;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Error
{
    public class ErrorSender : MonoBehaviour
    {
        [SerializeField] private EventChannelSO eventChannel;
        
        public void Error(string eM)
        {
            eventChannel.Raise(new OnError(eM));
        }
    }
}