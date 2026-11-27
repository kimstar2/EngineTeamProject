using UnityEngine;
using UnityEngine.Events;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program
{
    public class ProgramOpener : AbstractOpener
    {
        [SerializeField] private UnityEvent onOpenProgram;
        public override void OpenProgram()
        {
            onOpenProgram?.Invoke();
        }
    }
}