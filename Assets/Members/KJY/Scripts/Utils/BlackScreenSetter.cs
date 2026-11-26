using System;
using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Core.Services;
using Members.KJY.Scripts.Utils.TweenUtils;
using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    public class BlackScreenSetter : MonoBehaviour
    {
        [SerializeField] private TweenMotion tweenMotion;
        private IScreenManager _screenManager;

        private void Start()
        {
            _screenManager = ServiceLocator.GetService<IScreenManager>();
        }

        public void RayBlock(bool b)
        {
            _screenManager.SetBlackScreenRayBlock(b);
        }
        
        public void AlphaSet(float a)
        {
            _screenManager.SetBlackScreenAlpha(tweenMotion, a);
        }
    }
}