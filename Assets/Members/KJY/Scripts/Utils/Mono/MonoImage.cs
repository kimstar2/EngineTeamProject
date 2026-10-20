using System;
using UnityEngine;
using UnityEngine.UI;

namespace Members.KJY.Scripts.Utils.Mono
{
    public class MonoImage : MonoBehaviour
    {
        public Image Image {get; private set;}

        private void Awake()
        {
            Image = GetComponent<Image>();
        }

        public void SetImage(Sprite sprite)
        {
            Image.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            Image.color = color;
        }
        
        public void SetEnable(bool enable)
        {
            Image.enabled = enable;
        }
    }
}