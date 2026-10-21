using System;
using System.Collections.Generic;
using Members.KJY.Scripts.Check;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Application
{
    public class ApplicationManager : MonoBehaviour , IRequiredPooling
    {
        [SerializeField] private Transform parentTransform;
        [SerializeField] private Application applicationPrefab; // TODO : 풀링
        [SerializeField] private List<ProgramInfoSO> defaultApplications;
        private ApplicationOpener _currentOpener;

        private void Awake()
        {
            Make();
        }

        private void Make()
        {
            foreach (var info in defaultApplications)
            { 
                Application application = Instantiate(applicationPrefab, parentTransform);
                application.Init(info);
                application.Opener.OnClicked += HandleClick;
            }
        }

        private void HandleClick(ApplicationOpener obj)
        {
            if (_currentOpener != null)
                _currentOpener.ExitClick();
            _currentOpener = obj;
        }
    }
}