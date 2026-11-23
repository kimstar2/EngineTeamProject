using _TevLib.Extension.DoT;
using DG.Tweening;
using UnityEngine;

namespace Members.KJY.Scripts.Login
{
    public class LoginUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        [Header("Tween Setting")]
        [SerializeField] private float waitTime;
        [SerializeField] private Ease fadeEase;
        [SerializeField] private float fadeDuration;
        private Sequence _loginSeq;
        private bool _isLogin;
        
        public void OnLogin()
        {
            if (_isLogin) return;
            _isLogin = true;
            
            _loginSeq?.Kill();
            _loginSeq = DOTween.Sequence();
            _loginSeq.AppendInterval(waitTime);
            _loginSeq.Append(canvasGroup.DOFade(0, fadeDuration).SetEase(fadeEase));
            _loginSeq.AppendCallback(() => canvasGroup.blocksRaycasts = false);
        }
    }
}