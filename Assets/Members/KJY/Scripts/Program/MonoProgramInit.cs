using Members.KJY.Scripts.Program.System;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program
{
    public abstract class MonoProgramInit : MonoBehaviour , IProgramInit
    {
        public ProgramData ProgramData { get; private set; }
        public virtual void ProgramInit(ProgramData programData)
        {
            ProgramData = programData;
        }
    }
}