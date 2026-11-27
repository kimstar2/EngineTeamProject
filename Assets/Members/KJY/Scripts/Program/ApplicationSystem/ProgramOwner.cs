using System;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Core.Services;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.Func.FuncFileExplorer;
using Members.KJY.Scripts.Program.System;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program.ApplicationSystem
{
    public class ProgramOwner : MonoBehaviour
    {
        [SerializeField] private bool isFile;
        [HideIf("isFile")]
        [SerializeField] private ProgramInfoSO programInfo;
        [HideIf("isFile")]
        [SerializeField] private ProgramEntry programEntry;
        [ShowIf("isFile")]
        [SerializeField] private ProgramInfoSO fileBase;
        [ShowIf("isFile")]
        [SerializeField] private FileEntry fileEntry;
        [ShowIf("isFile")]
        [SerializeField] private FuncFileExplorer fileExplorerBase;
        public ProgramController MyProgram { get; private set; }    
        private IProgramManager _programManager;

        private void Start()
        { 
            _programManager = ServiceLocator.GetService<IProgramManager>();
        }

        public void Make()
        {
            if (isFile)
            {
                FuncFileExplorer explorer = Instantiate(fileExplorerBase);
                explorer.SetFileData(fileEntry);
                ProgramInfoSO realInfo = Instantiate(fileBase);
                realInfo.SetName(fileEntry.ProgramName);
                realInfo.AddFunc(explorer);
                MyProgram = _programManager.MakeChain(realInfo, fileEntry);
            }
            else
                MyProgram = _programManager.MakeChain(programInfo, programEntry);
        }

        public void CloseProgram()
        {
            if (MyProgram == null) return;
            
            if (MyProgram.ProgramReadonlyDict.TryGetValue(ProgramState.Closed, out ProgramSystemBase close))
            {
                close.ExecuteSystem();
                MyProgram = null;
            }
        }
    }
}