using System;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Core.Services;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Application
{
    public class ApplicationUnClicked : MonoBehaviour
    {
        private IApplicationManager _applicationManager;

        private void Start()
        {
            _applicationManager = ServiceLocator.GetService<IApplicationManager>();
        }

        public void UnClick() => _applicationManager.UnClick();
    }
}