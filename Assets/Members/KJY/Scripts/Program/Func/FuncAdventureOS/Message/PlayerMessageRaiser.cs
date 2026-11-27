using System.Collections.Generic;
using _TevLib.ServiceLocatorSystem;
using UnityEditor.Animations;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class PlayerMessageRaiser : MonoBehaviour
    {
        [SerializeField] private MessageListSO message;
        [SerializeField] private List<ProgramSystems> systems;
        
        public void Raise()
        {
            ServiceLocator.GetService<IMessageRaiser>().PlayerRaise(message,Vector2.zero,systems);
        }
    }
}