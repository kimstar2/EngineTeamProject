using System;
using TMPro;
using UnityEngine;

namespace Members.KJY.Scripts.Utils.Mono
{
    public class MonoTMP_UGUI : MonoBehaviour
    {
        public TextMeshProUGUI TMP { get; private set; }

        private void Awake()
        {
            TMP = GetComponent<TextMeshProUGUI>();
        }

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