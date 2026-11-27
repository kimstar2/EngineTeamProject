using System;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Core.Services;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program
{
    public class ProgramAddedRaiser : MonoBehaviour
    {
        [Header("User Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private Sprite programIcon;
        [SerializeField] private string addName;
        private IProgramAddedManager _programAddedManager;

        private void Start()
        {
            _programAddedManager = ServiceLocator.GetService<IProgramAddedManager>();
        }

        public void Raise()
        {
            _programAddedManager.ProgramRaise(programIcon, addName);
        }
    }
}