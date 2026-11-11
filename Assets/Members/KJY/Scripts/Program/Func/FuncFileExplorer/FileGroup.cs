using System.Collections.Generic;
using UnityEngine;
using Application = Members.KJY.Scripts.Program.ApplicationSystem.Application;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FileGroup : MonoBehaviour
    {
        [field:SerializeField] public FilePathBar PathBar { get; private set; }
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
            PathBar.MakePath(fileName);
            ClearCurrent();
        }

        public void ClearCurrent()
        {
            foreach (Application app in _crtApps)
                Destroy(app.gameObject);
            foreach (FileCell file in _crtFiles)
                Destroy(file.gameObject);
            _crtApps?.Clear();
            _crtFiles?.Clear();
        }
    }
}