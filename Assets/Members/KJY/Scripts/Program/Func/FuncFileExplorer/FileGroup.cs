using System.Collections.Generic;
using UnityEngine;
using Application = Members.KJY.Scripts.Program.ApplicationSystem.Application;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public readonly struct CellGroup
    {
        public readonly List<Application> AppList;
        public readonly List<FileCell> FileList;

        public CellGroup(List<FileCell> fileList, List<Application> appList)
        {
            FileList = fileList;
            AppList = appList;
        }
    }
    public class FileGroup : MonoBehaviour
    {
        [SerializeField] private FilePathBar pathBar;
        private readonly Stack<CellGroup> _undoCellGroups = new();
        private readonly List<Application> _crtApps = new();
        private readonly List<FileCell> _crtFiles = new();
        
        
        public void AddCell(Application app)
        {
            _crtApps.Add(app);
        }
        
        public void AddCell(FileCell file)
        {
            _crtFiles.Add(file);
        }

        public void MakeNew(string fileName)
        {
            CellGroup cellGroup = new CellGroup(_crtFiles, _crtApps);
            
            foreach (Application app in _crtApps)
                Destroy(app.gameObject);
            foreach (FileCell file in _crtFiles)
                Destroy(file.gameObject);
            
            _crtApps?.Clear();
            _crtFiles?.Clear();
            
            pathBar.MakePath(fileName);
            _undoCellGroups.Push(cellGroup);
        }
    }
}