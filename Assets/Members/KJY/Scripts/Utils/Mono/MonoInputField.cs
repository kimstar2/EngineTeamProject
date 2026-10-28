using Assets.Members.KJY.Scripts.Utils.Mono;
using TMPro;
using UnityEngine;

namespace Members.KJY.Scripts.Utils.Mono
{
    public class MonoInputField : MonoBase<TMP_InputField>
    {
        public TMP_InputField InputField => compo;

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