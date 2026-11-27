using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Entry;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace Members.KJY.Scripts.Core.Services
{
    public class ProgramAddedManager : MonoBehaviour, IProgramAddedManager
    {
        [Header("Program Added Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private ProgramAdded addedPrefab;
        [SerializeField] private Transform addedParent;
        [SerializeField] private string addedProgram;
        [SerializeField] private UnityEvent onAdded;

        public void ProgramRaise(ProgramInfoSO info, ProgramEntry entry)
        {
            ProgramAdded programAdded = Instantiate(addedPrefab, addedParent);
            programAdded.Set(info.ProgramIcon,addedProgram,entry.GetProgramName(info));
            onAdded?.Invoke();
        }

        public void ProgramRaise(Sprite sprite, string programName)
        {
            ProgramAdded programAdded = Instantiate(addedPrefab, addedParent);
            programAdded.Set(sprite,addedProgram,programName);
            onAdded?.Invoke();
        }
    }
}