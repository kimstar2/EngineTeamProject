using System;
using _TevLib.HashDataSystem;
using NaughtyAttributes;
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

        [field:SerializeField] public bool UseEvent { get; private set; }
        
        [field:AllowNesting] // 이중
        [field:ShowIf(nameof(UseEvent))]
        [field:SerializeField] public MessageEventSO Event { get; private set; }
        
        [field:SerializeField] public bool UseAdvanceEvent { get; private set; }
        
        [field:AllowNesting]
        [field:ShowIf(nameof(UseAdvanceEvent))]
        [field:SerializeField] public AdvanceEventSO AdvanceEvent { get; private set; }
    }
}
