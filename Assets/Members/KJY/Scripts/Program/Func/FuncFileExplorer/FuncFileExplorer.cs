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

        public void SetFileData(FileSO fileData)
        {
            FileData = fileData;
        }
        
        public override void Init()
        {
            fileGroup.MakeNew(FileData.FileName);
            foreach (FileSO fileData in FileData.FileList)
                MakeFile(fileData);
            foreach (ProgramInfoSO programData in FileData.ProgramList)
                MakeApp(programData);
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
            fileGroup.MakeNew(newFile.FileName);
            foreach (FileSO fileData in newFile.FileList)
                MakeFile(fileData);
            foreach (ProgramInfoSO programData in newFile.ProgramList)
                MakeApp(programData);
        }

        private void OnDestroy()
        {
            foreach (FileCell cell in _fileList)
                cell.Opener.OnInFile -= HandleInFile;
        }
    }
}