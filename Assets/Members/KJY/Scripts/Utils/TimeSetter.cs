using System;
using _TevLib.ServiceLocatorSystem;
using _TevLib.ServiceLocatorSystem.TimeService;
using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    public class TimeSetter : MonoBehaviour
    {
        private ITimeService _timeService;

        private void Start()
        {
            _timeService = ServiceLocator.GetService<ITimeService>();
        }

        public void SetFixedDeltaTime(float deltaTime)
        {
            Time.fixedDeltaTime = deltaTime;
        }

        public void SetTime(float t)
        {
            _timeService.SetTimeScale(t);
        }
    }
}