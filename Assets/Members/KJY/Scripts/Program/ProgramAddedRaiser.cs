using System;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Core.Services;
using Members.KJY.Scripts.Program.Entry;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program
{
    public class ProgramAddedRaiser : MonoBehaviour
    {
        [Header("User Setting")] [HorizontalLine(color: EColor.Gray)]
        
        [SerializeField] private bool useSO;
        [HideIf(nameof(useSO))]
        [SerializeField] private Sprite programIcon;
        [HideIf(nameof(useSO))]
        [SerializeField] private string addName;
        
        [ShowIf(nameof(useSO))]
        [SerializeField] private ProgramInfoSO info;
        [ShowIf(nameof(useSO))]
        [SerializeField] private ProgramEntry entry;
        
        
        private IProgramAddedManager _programAddedManager;

        private void Start()
        {
            _programAddedManager = ServiceLocator.GetService<IProgramAddedManager>();
        }

        public void Raise()
        {
            if (useSO)
                _programAddedManager.ProgramRaise(info,entry);
            else
                _programAddedManager.ProgramRaise(programIcon, addName);
        }
    }
}