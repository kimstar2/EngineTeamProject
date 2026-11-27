using System;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Utils
{
    [Serializable]
    public struct WaitSequenceGroup
    {
        public float time;
        public UnityEvent action;
    }
}