using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Members.KJY.Scripts.Utils;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FilePathBar : MonoBehaviour
    {
        [SerializeField] private Transform containerParent;
        [SerializeField] private FilePath filePathPrefab;

        private bool _hasFirstPath;
        private readonly List<(FilePath space,FilePath real)> _filePaths = new(); 
        private const string FirstPath = "C: ";
        private const string SpacePath = " > ";
        
        public event Action<int> OnRemake;
        
        public void MakePath(string fileName)
        {
            FilePath spacePath = Instantiate(filePathPrefab, containerParent);
            spacePath.SetCanClick(false);
            
            if (_hasFirstPath)
                spacePath.SetPath(SpacePath);
            else
            {
                spacePath.SetPath(FirstPath);
                _hasFirstPath = true;
            }
            
            FilePath filePath = Instantiate(filePathPrefab,containerParent);
            
            _filePaths.Add((spacePath,filePath));
            
            filePath.SetPath(fileName);
            filePath.SetCanClick(true);
            filePath.SetMyNum(_filePaths.Count);
            
            filePath.OnClick += HandleClickPath;
        }

        private void RemovePath()
        {
            (FilePath space, FilePath real) paths = _filePaths[^1];
            _filePaths.RemoveAt(_filePaths.Count - 1);
            paths.real.OnClick -= HandleClickPath;
            Destroy(paths.space.gameObject);
            Destroy(paths.real.gameObject);
        }

        private void HandleClickPath(int num)
        {
            if (num == _filePaths.Count) return;
            int cnt = _filePaths.Count - num;
            
            for (int i = 0; i < cnt; i++)
                RemovePath();
            
            OnRemake?.Invoke(num);   
        }
    }
}