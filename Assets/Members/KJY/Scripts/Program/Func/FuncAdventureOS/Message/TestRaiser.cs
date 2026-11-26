using _TevLib.ServiceLocatorSystem;
using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class TestRaiser : MonoBehaviour
    {
        [SerializeField] private AnimatorController test;
        [SerializeField] private MessageListSO testMessage;
        
        [ContextMenu("text")]
        public void Raise()
        {
            ServiceLocator.GetService<IMessageRaiser>().Raise(test,testMessage,Vector2.zero);
        }
    }
}