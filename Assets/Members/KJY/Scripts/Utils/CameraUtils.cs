using System;
using UnityEngine;

namespace Scenes.Members.KJY.Scripts.Utils
{
    public class CameraUtils : MonoBehaviour
    {
        public static Camera MainCam {get; private set;}
        
        private void Awake()
        {
            MainCam = Camera.main;
        }
    }
}