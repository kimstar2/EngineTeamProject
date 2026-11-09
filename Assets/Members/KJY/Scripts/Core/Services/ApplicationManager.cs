using System;
using System.Collections.Generic;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Check;
using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.ApplicationSystem;
using UnityEngine;
using Application = Members.KJY.Scripts.Program.ApplicationSystem.Application;

namespace Members.KJY.Scripts.Core.Services
{
    public class ApplicationManager : MonoBehaviour , IApplicationManager , IRequiredPooling
    {
        [SerializeField] private Transform parentTransform;
        [SerializeField] private Application applicationPrefab; // TODO : 풀링
        [SerializeField] private List<ProgramInfoSO> defaultApplications;
        private readonly List<AbstractOpener> _applicationList  = new();
        private AbstractOpener _currentApplication;

        private void OnDestroy()
        {
            foreach (AbstractOpener app in _applicationList)
                app.OnClicked -= HandleClick;
        }

        public void MakeDefaults()
        {
            foreach (var info in defaultApplications)
                Make(info);
        }


        public void Make(ProgramInfoSO info)
        {
            Application application = Instantiate(applicationPrefab, parentTransform);
            application.Init(info);
            application.Opener.OnClicked -= HandleClick;
            application.Opener.OnClicked += HandleClick;
            _applicationList.Add(application.Opener);
        }

        #region Handle
        
        private void HandleClick(AbstractOpener obj)
        {
            if (obj == _currentApplication) return;
            if (_currentApplication != null)
                _currentApplication.ExitClick();
            _currentApplication = obj;
        }

        #endregion

        #region Helper

        public void UnClick()
        {
            _currentApplication?.ExitClick();
            _currentApplication = null;
        }
        
        #endregion
    }
}