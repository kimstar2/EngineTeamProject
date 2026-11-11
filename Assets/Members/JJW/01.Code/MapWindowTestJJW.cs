using Members.KJY.Scripts.Core.Services;
using Members.KJY.Scripts.Program;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Members.JJW._01.Code
{
    public class MapWindowTestJJW : MonoBehaviour
    {
        [SerializeField] private ProgramManager programManager;
        [SerializeField] private ProgramInfoSO secondMapData;

        private void Update()
        {
            if (Keyboard.current == null
                || programManager == null
                || secondMapData == null)
            {
                return;
            }

            if (Keyboard.current.nKey.wasPressedThisFrame)
            {
                programManager.MakeChain(secondMapData);
            }
        }
    }
}