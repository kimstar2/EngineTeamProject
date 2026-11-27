using UnityEngine;
using UnityEngine.Serialization;

namespace Members.KJY.Scripts.Program.System
{
    public class SystemStateSelector : MonoBehaviour, IProgramInit
    {
        [FormerlySerializedAs("_programController"), SerializeField] private ProgramController programController;
        [SerializeField] private ProgramState programState;

        [SerializeField] private GameObject buttonRoot;
        public ProgramData ProgramData { get; private set; }

        public void ProgramInit(ProgramData programData)
        {
            ProgramData = programData;

            if (buttonRoot != null)
            {
                var systems = programData.Entry.GetSystems(programData.ProgramInfo);
                buttonRoot.SetActive(systems.Exists(system => system.ProgramState == programState && !system.HideButton));
            }
        }

        public void Select()
        {
            programController.ChangeState(SystemInvoker.ProgramController,programState);
        }
        
        #if UNITY_EDITOR

        private void OnValidate()
        {
            gameObject.name = $"ChangeStateTo_{programState}";
        }

#endif
    }
}
