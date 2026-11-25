using System;
using _TevLib.HashDataSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    [Serializable]
    public struct MessageGroup
    {
        [field: SerializeField] public AnimHashSO AnimHash { get; private set; }
        [field:SerializeField] public string Name { get; private set; }
        [field:TextArea]
        [field:SerializeField] public string Message { get; private set; }
        [field:SerializeField] public UnityEvent Event {get; private set;}
    }
}