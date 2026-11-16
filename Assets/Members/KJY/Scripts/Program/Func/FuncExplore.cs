using System.Collections.Generic;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func
{
    public class FuncExplore : ProgramFunc
    {
        [SerializeField] private SearchSystem searchView;

        public void SetData(List<SearchData> dataList)
        {
            searchView.SetSearchData(dataList.ToArray());
        }
    }
}