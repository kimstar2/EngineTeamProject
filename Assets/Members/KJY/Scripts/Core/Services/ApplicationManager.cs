using System;
using System.Collections.Generic;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Check;
using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.ApplicationSystem;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.Func.FuncFileExplorer;
using NaughtyAttributes;
using UnityEngine;
using ZLinq.Linq;
using Application = Members.KJY.Scripts.Program.ApplicationSystem.Application;

namespace Members.KJY.Scripts.Core.Services
{
    public class ApplicationManager : MonoBehaviour , IApplicationManager , IRequiredPooling
    {
        [Header("Application Setting")]
        [SerializeField] private Transform parentTransform;
        [SerializeField] private Application applicationPrefab; // TODO : 풀링
        [SerializeField] private List<ProgramEntry> defaultEntryList = new List<ProgramEntry>();
        [SerializeField] private ProgramListSO applicationList;
        private readonly List<AbstractOpener> _applicationList  = new();
        private AbstractOpener _currentApplication;

        private readonly Dictionary<ProgramType, ProgramInfoSO> _programDict = new();
        public IReadOnlyDictionary<ProgramType, ProgramInfoSO> ProgramDict => _programDict;
        
        [Header("File Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private ProgramInfoSO fileInfoBase;
        [SerializeField] private FuncFileExplorer fileExplorerBase;
        
        private void OnDestroy()
        {
            foreach (AbstractOpener app in _applicationList)
                app.OnClicked -= HandleClick;
        }

        public void Init()
        {
            foreach (var info in applicationList.ProgramList)
                _programDict.Add(info.programType,info.program);
        }

        public void MakeDefaults()
        {
            foreach (var entry in defaultEntryList)
            {
                if (!_programDict.TryGetValue(entry.BaseProgram, out ProgramInfoSO info)) continue;
                if (entry.BaseProgram == ProgramType.File)
                    MakeFile((FileEntry)entry);
                else
                    Make(info , entry);
            }
        }


        public void Make(ProgramInfoSO info , ProgramEntry entry)
        {
            Application application = Instantiate(applicationPrefab, parentTransform);
            application.name = entry.name;
            application.Init(info, entry);
            application.Opener.OnClicked -= HandleClick;
            application.Opener.OnClicked += HandleClick;
            _applicationList.Add(application.Opener);
        }
        
        public void MakeFile(FileEntry fileEntry)
        {
            FuncFileExplorer explorer = Instantiate(fileExplorerBase);
            explorer.SetFileData(fileEntry);

            ProgramInfoSO realInfo = Instantiate(fileInfoBase);
            realInfo.SetName(fileEntry.ProgramName);
            realInfo.AddFunc(explorer);
                
            Make(realInfo , fileEntry);
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