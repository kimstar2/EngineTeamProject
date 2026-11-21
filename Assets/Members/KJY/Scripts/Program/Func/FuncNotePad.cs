using System;
using _TevLib.CoreLib;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func
{
    public class FuncNotePad : ProgramFunc
    {
        [Header("Text Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private MonoInputField monoField;
        private NotePadDataSO _notePadData;
        
        [Header("Font Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private int defaultFontSizeIndex;
        [SerializeField] private float[] fontSizes;
        private readonly NotifyValue<float> _crtFontSize = new();
        private int _crtFontSizeIndex;

        private int FontSizeIndex
        {
            get => _crtFontSizeIndex;
            set
            {
                _crtFontSizeIndex = Mathf.Clamp(value, 0, fontSizes.Length - 1);
                _crtFontSize.Value = fontSizes[_crtFontSizeIndex];
                monoField.SetFontSize(FSize);
            }
        }
        private float FSize => _crtFontSize.Value; // FontSize

        public void SetNotePadEntry(NotepadEntry data)
        {
            _notePadData = data.NotepadData;
        }
        
        public override void Init()
        {
            monoField.SetText(_notePadData.DefaultContent);
            monoField.SetWarp(_notePadData.TextWrapMode);
            FontSizeIndex = defaultFontSizeIndex;
        }

        public void UpFontSize()
        {
            ++FontSizeIndex;
        }

        public void DownFontSize()
        {
            --FontSizeIndex;
        }
    }
}