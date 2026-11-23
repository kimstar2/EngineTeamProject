using _TevLib.ServiceLocatorSystem;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class TestRaiser : MonoBehaviour
    {
        [SerializeField] private PCData pcData;
        [SerializeField] private string testMessage;
        
        [ContextMenu("text")]
        public void Raise()
        {
            ServiceLocator.GetService<IMessageRaiser>().Raise(pcData,testMessage,Vector2.zero);
        }
    }
}