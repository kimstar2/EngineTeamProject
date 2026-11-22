using Members.KJY.Scripts.Program.Entry;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Members.KJY.Scripts.Program.Entry
{
    [CreateAssetMenu(fileName = "Internet Entry", menuName = "KJY/Program/Program Entry/Internet Entry", order = 0)]
    public class InternetEntry : ProgramEntry
    {
        [field: SerializeField] public List<SearchData> SearchList{ get; private set; }
    }
}
