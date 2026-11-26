using _TevLib.ServiceLocatorSystem;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message.MessageEventReceiver
{
    public abstract class MessageEventReceiver<T> : MonoBehaviour
    {
        [field:Header("Event Setting")]
        [field:HorizontalLine(color:EColor.Gray)]
        [field:SerializeField] public MessageEventSO MessageEvent {get; private set;}
        [field:SerializeField] public UnityEvent<T> OnMessageInvoked {get; private set;}
        private IMessageRaiser _messageRaiser;

        protected virtual void OnInit()
        {
            _messageRaiser = ServiceLocator.GetService<IMessageRaiser>();
        }

        public void OnEnable()
        {
            OnInit();
            _messageRaiser.RegisterEvent(MessageEvent,HandleInvokedMessage);
        }

        public void OnDisable()
        {
            _messageRaiser.UnRegisterEvent(MessageEvent, HandleInvokedMessage);
        }

        private void HandleInvokedMessage(ProgramController pc)
        {
            OnMessageInvoked?.Invoke(ReturnInvokeType(pc));
        }

        protected abstract T ReturnInvokeType(ProgramController pc);
    }
}