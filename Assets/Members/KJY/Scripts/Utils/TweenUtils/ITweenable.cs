using DG.Tweening;

namespace Members.KJY.Scripts.Utils.TweenUtils
{
    public interface ITweenable
    {
        Tween CurrentTween { get; }
        void ExecuteTween();
    }
}
