using TMPro;
using UnityEngine;

namespace Members.KJY.Scripts.Utils.Mono
{
    public class MonoInputField : MonoBehaviour
    {
        [field:SerializeField] public TMP_InputField InputField { get; private set; }

        public void SetFontSize(float size) => SetPointSize(size);
        public void SetPointSize(float size) => InputField.pointSize = size;

        public void SetText(string text)
        {
            InputField.text = text;
        }

        public void SetWarp(TextWrappingModes wrap)
        {
            InputField.textComponent.textWrappingMode = wrap;
        }
    }
}