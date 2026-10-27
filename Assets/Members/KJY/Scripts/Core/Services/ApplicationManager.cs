using System.Collections.Generic;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Check;
using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Application;
using UnityEngine;
using Application = Members.KJY.Scripts.Program.Application.Application;

namespace Members.KJY.Scripts.Core.Services
{
    public class ApplicationManager : MonoBehaviour , IApplicationManager , IRequiredPooling
    {
        [SerializeField] private Transform parentTransform;
        [SerializeField] private Application applicationPrefab; // TODO : 풀링
        [SerializeField] private List<ProgramInfoSO> defaultApplications;
        private readonly List<ApplicationOpener> _applicationList  = new();
        private ApplicationOpener _currentApplication;

        private void Awake()
        {
            Make();
            
            ServiceLocator.RegisterService<IApplicationManager>(this);
        }

        private void OnDestroy()
        {
            foreach (ApplicationOpener app in _applicationList)
                app.OnClicked -= HandleClick;
            
            ServiceLocator.UnregisterService<IApplicationManager>();
        }

        private void Make()
        {
            foreach (var info in defaultApplications)
                Make(info);
        }


        public void Make(ProgramInfoSO info)
        {
            Application application = Instantiate(applicationPrefab, parentTransform);
            application.Init(info);
            application.Opener.OnClicked += HandleClick;
            _applicationList.Add(application.Opener);
        }

        #region Handle
        
        private void HandleClick(ApplicationOpener obj)
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