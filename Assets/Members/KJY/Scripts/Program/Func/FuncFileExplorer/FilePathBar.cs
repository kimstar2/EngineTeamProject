using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FilePathBar : MonoBehaviour
    {
        [SerializeField] private FilePath filePath;
        
        private readonly List<string> _filePaths = new(); 
        private const string FirstPath = "C: ";
        private const string SpacePath = " > ";
        
        public void MakePath(string fileName)
        {
            _filePaths.Add(fileName);

            StringBuilder sB = new();
            StringWriter writer = new(sB);
            sB.Append(FirstPath);
            
            foreach (string path in _filePaths)
                sB.Append(path);
            
            sB.Remove(sB.Length - SpacePath.Length, sB.Length);
            
            writer.Write();
        }
    }
}