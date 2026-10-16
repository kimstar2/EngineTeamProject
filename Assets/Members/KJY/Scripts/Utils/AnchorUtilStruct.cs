using System;
using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    [Serializable]
    public struct AnchorUtilStruct
    {
        [field:SerializeField] public Vector2 MinAnchor {get; private set;}
        [field:SerializeField] public Vector2 MaxAnchor {get; private set;}
        [field:SerializeField] public Vector2 Pivot {get; private set;}
    }
}