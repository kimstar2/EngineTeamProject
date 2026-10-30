using System.Collections.Generic;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncInternet
{
    public class FuncInternetVisualizer : MonoBehaviour
    {
        [Header("Theme Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private TMP_Dropdown themeDropdown;
        [SerializeField] private ThemeSO[] themeList;
        [Header("Visual Setting")]
        [SerializeField] private MonoC[] mainColors;
        [SerializeField] private MonoC[] subColors1;
        [SerializeField] private MonoC[] subColors2;

        private readonly List<ISetColorable> mainColor_cache = new(), subColor1_cache = new(), subColor2_cache = new();

        private void Awake()
        {
            mainColor_cache.Clear();
            subColor1_cache.Clear();
            subColor2_cache.Clear();

            CacheSetColorList();
        }

        private void OnEnable()
        {
            themeDropdown.onValueChanged.AddListener(HandleDropValueChanged);

            themeDropdown.ClearOptions();

            foreach (var theme in themeList)
            {
                TMP_Dropdown.OptionData option = new()
                {
                    text = theme.name,
                };

                themeDropdown.options.Add(option);
            }
        }

        private void OnDisable()
        {
            themeDropdown.onValueChanged.RemoveListener(HandleDropValueChanged);
        }

        private void HandleDropValueChanged(int index)
        {
            if (themeList.Length - 1 < index) return;

            ThemeSO theme = themeList[index];

            foreach (var main in mainColor_cache)
                main.SetColor(theme.MainColor);
            foreach (var sub1 in subColor1_cache)
                sub1.SetColor(theme.SubColor1);
            foreach (var sub2 in subColor2_cache)
                sub2.SetColor(theme.SubColor2);
        }

        #region Helper

        private void CacheSetColorList()
        {
            foreach (var c in mainColors)
            {
                if (c.TryGetComponent(out ISetColorable setColorable))
                    mainColor_cache.Add(setColorable);
            }
            foreach (var c in subColors1)
            {
                if (c.TryGetComponent(out ISetColorable setColorable))
                    subColor1_cache.Add(setColorable);
            }
            foreach (var c in subColors2)
            {
                if (c.TryGetComponent(out ISetColorable setColorable))
                    subColor2_cache.Add(setColorable);
            }
        }


        #endregion
    }
}
