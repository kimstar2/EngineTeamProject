using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program.System
{
    public class ProgramDisable : ProgramSystemBase
    {
        [Header("Tween Setting")] [HorizontalLine(color: EColor.Gray)] 
        [SerializeField] private Ease tweenEase = Ease.Linear;
        [SerializeField] private float tweenDuration;

        public override void ExecuteSystem()
        {
            base.ExecuteSystem();
            Disable();
        }

        private void Disable()
        {
            ProgramCon.ProgramBar.OnExit?.Invoke();
            ProgramCon.OnExit?.Invoke();
        }
    }
}