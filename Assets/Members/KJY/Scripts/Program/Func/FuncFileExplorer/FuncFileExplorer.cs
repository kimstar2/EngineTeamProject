using System.Collections.Generic;
using Members.KJY.Scripts.Check;
using UnityEngine;
using Application = Members.KJY.Scripts.Program.ApplicationSystem.Application;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FuncFileExplorer : ProgramFunc , IRequiredPooling
    {
        [field:SerializeField] public FileSO FileData { get; private set; }
        [SerializeField] private FileCell fileCellPrefab;
        [SerializeField] private Application applicationPrefab;
        [SerializeField] private FileGroup fileGroup;
        private readonly List<FileCell> _fileList = new();
        private readonly List<FileSO> _fileDataList = new();

        public void SetFileData(FileSO fileData)
        {
            FileData = fileData;
        }
        
        public override void Init()
        {
            fileGroup.MakeNew(FileData.FileName);
            _fileDataList.Add(FileData);
            
            foreach (FileSO fileData in FileData.FileList)
                MakeFile(fileData);
            foreach (ProgramInfoSO programData in FileData.ProgramList)
                MakeApp(programData);
        }

        private void OnEnable()
        {
            fileGroup.PathBar.OnRemake += Remake;
        }
        private void OnDisable()
        {
            fileGroup.PathBar.OnRemake -= Remake;
        }

        public void MakeApp(ProgramInfoSO programData)
        {
            Application app = Instantiate(applicationPrefab, fileGroup.transform);
            app.Init(programData);
            
            fileGroup.AddCell(app);
        }

        public void MakeFile(FileSO fileData)
        {
            FileCell file = Instantiate(fileCellPrefab, fileGroup.transform);
            file.Init(fileData);
            
            fileGroup.AddCell(file);
            _fileList.Add(file);
            
            file.Opener.OnInFile += HandleInFile;
        }

        private void HandleInFile(FileSO newFile)
        {
            _fileDataList.Add(newFile);
            fileGroup.MakeNew(newFile.FileName);
            foreach (FileSO fileData in newFile.FileList)
                MakeFile(fileData);
            foreach (ProgramInfoSO programData in newFile.ProgramList)
                MakeApp(programData);
        }

        private void Remake(int num)
        {
            int cnt = _fileDataList.Count - num;
            for (int i = 0; i < cnt; i++)
                _fileDataList.RemoveAt(_fileDataList.Count-1);
            
            fileGroup.ClearCurrent();
            
            foreach (FileSO fileData in _fileDataList[^1].FileList)
                MakeFile(fileData);
            foreach (ProgramInfoSO programData in _fileDataList[^1].ProgramList)
                MakeApp(programData);
        }
        
        private void OnDestroy()
        {
            foreach (FileCell cell in _fileList)
                cell.Opener.OnInFile -= HandleInFile;
        }
        
    }
}