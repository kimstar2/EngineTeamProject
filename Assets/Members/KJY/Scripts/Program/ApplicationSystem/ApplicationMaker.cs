using System;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Core.Services;
using Members.KJY.Scripts.Program.Entry;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program.ApplicationSystem
{
    public class ApplicationMaker : MonoBehaviour
    {
        [SerializeField] private bool isFile;
        [HideIf("isFile")]
        [SerializeField] private ProgramInfoSO programInfo;
        [HideIf("isFile")]
        [SerializeField] private ProgramEntry programEntry;
        [ShowIf("isFile")]
        [SerializeField] private FileEntry fileEntry;
        private IApplicationManager _applicationManager;

        private void Start()
        { 
            _applicationManager = ServiceLocator.GetService<IApplicationManager>();
        }

        public void Make()
        {
            if (isFile)
                _applicationManager.MakeFile(fileEntry);
            else
                _applicationManager.Make(programInfo, programEntry);
        }
    }
}