using System;
using UnityEngine;

namespace Assets.Members.KJY.Scripts.Program.Func
{
    public abstract class ProgramFunc : MonoBehaviour
    {
        public virtual void Init() {}

        private void OnValidate()
        {
            gameObject.name = name;
        }

        [ContextMenu("Validate")]
        public void Validate() => OnValidate();
    }
}