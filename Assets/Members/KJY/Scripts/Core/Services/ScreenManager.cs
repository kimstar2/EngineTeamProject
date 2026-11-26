using DG.Tweening;
using Members.KJY.Scripts.Utils.Mono;
using Members.KJY.Scripts.Utils.TweenUtils;
using UnityEngine;

namespace Members.KJY.Scripts.Core.Services
{
    public class ScreenManager : MonoBehaviour , IScreenManager
    {
        [SerializeField] private MonoImage blackScreen;
        private Tween _blackScreenTween;
        
        public void SetBlackScreenAlpha(TweenMotion m, float alpha)
        {
            _blackScreenTween?.Kill();
            _blackScreenTween = blackScreen.Image.DOFade(alpha, m.duration).SetEase(m.ease);
        }

        public void SetBlackScreenRayBlock(bool b)
        {
            blackScreen.Image.raycastTarget = b;
        }
    }
}