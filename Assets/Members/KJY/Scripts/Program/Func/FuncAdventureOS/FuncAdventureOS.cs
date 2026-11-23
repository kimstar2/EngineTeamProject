using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS
{
    public class FuncAdventureOS : ProgramFunc
    {
        [Header("AdventureOS Life Cycle")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private FuncAdvOS_Intro intro;
        [SerializeField] private FuncAdvOS_Start start;
        public override void Init()
        {
            base.Init();
            intro.StartIntro();
            start.Init();
        }
    }
}