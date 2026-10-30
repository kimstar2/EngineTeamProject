using System;
using TMPro;
using UnityEngine;

namespace Members.KJY.Scripts.Utils.Mono
{
    public class MonoTMP_UGUI : MonoBase<TextMeshProUGUI> , ISetColorable
    {
        public TextMeshProUGUI TMP => compo;

        public void SetText(string text)
        {
            TMP.SetText(text);
        }
        
        public void SetColor(Color color)
        {
            TMP.color = color;
        }
    }
}