using System;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message
{
    public class OnAdvanceEvent : MonoBehaviour
    {
        [SerializeField] private AdvanceEventSO advanceEvent;

        public void On()
        {
            advanceEvent.On();
        }

        public void OnDestroy()
        {
            advanceEvent.Reset();
        }
    }
}