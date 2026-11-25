using _TevLib.ServiceLocatorSystem;
using Members.JJW._01.Code;
using Members.KJY.Scripts.Program.Func.FuncAdventureOS.Message;
using NaughtyAttributes;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncAdventureOS
{
    public class FuncAdventureOS : ProgramFunc
    {
        [Header("AdventureOS Life Cycle")]
        [HorizontalLine(color:EColor.Gray)]
        [SerializeField] private FuncAdvOS_Intro intro;
        [SerializeField] private FuncAdvOS_Start start;

        [field:Header("In Game")] 
        [field:SerializeField] public FuncMapWindow FuncMapWindow { get; private set; }
        [SerializeField] private string[] startMessage;
        
        public override void Init()
        {
            base.Init();
            intro.StartIntro();
            start.Init();
            
            start.OnEndFade += HandleEndFade;
        }

        private void OnDestroy()
        {
            start.OnEndFade -= HandleEndFade;
        }

        private void HandleEndFade()
        {
            ServiceLocator.GetService<IMessageRaiser>().PlayerRaise(startMessage, new Vector2(0f,-250f));
        }
    }
}