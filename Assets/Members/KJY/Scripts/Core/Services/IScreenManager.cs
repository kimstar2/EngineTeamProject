using DG.Tweening.Core;
using Members.KJY.Scripts.Utils.TweenUtils;

namespace Members.KJY.Scripts.Core.Services
{
    public interface IScreenManager
    {
        public void SetBlackScreenAlpha(TweenMotion m,float alpha);
        void SetBlackScreenRayBlock(bool b);
    }
}