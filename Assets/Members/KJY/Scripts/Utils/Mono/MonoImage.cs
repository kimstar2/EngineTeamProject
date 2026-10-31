using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Members.KJY.Scripts.Utils.Mono
{
    public class MonoImage : MonoBase<Image>, ISetColorable
    {
        public Image Image => compo;

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

        public void SetFade(float fade)
        {
            Color temp = Image.color;
            temp.a = fade;
            Image.color = temp;
        }
    }
}