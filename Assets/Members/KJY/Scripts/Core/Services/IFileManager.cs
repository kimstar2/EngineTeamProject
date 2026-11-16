using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.Func.FuncFileExplorer;

namespace Members.KJY.Scripts.Core.Services
{
    public interface IFileManager
    {
        void MakeFile(FileEntry fileEntry);
    }
}