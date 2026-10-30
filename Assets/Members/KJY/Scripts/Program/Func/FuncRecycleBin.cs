using System.Collections.Generic;
using Members.KJY.Scripts.Check;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func
{
    public class FuncRecycleBin : ProgramFunc , IRequiredPooling
    {
        [Header("Cell Setting")] 
        [HorizontalLine(color:EColor.Gray)] 
        [SerializeField] private DeleteProgramCell deleteProgramCell; // TODO: 풀링
        [SerializeField] private Transform programCellGroup;
        [SerializeField] private List<DeleteProgramCell> deleteList = new();
        [SerializeField] private ProgramInfoSO test;

        public void DeleteAll()
        {
            foreach (DeleteProgramCell delete in deleteList)
            {
                Destroy(delete.gameObject); // TODO: 풀링
            }
            deleteList?.Clear();
        }

        [ContextMenu("Test")]
        public void Test() => AddDeleteList(test);
        
        public void AddDeleteList(ProgramInfoSO info)
        {
            DeleteProgramCell cellGo = Instantiate(deleteProgramCell, programCellGroup);
            cellGo.Init(info);
            deleteList.Add(cellGo);
        }
    }
}