using Assets.Members.KJY.Scripts.Utils.Mono;
using Members.KJY.Scripts.Utils.Mono;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Members.KJY.Scripts.Program.Func.FuncInternet
{
    public class FuncInternetVisualizer : MonoBehaviour
    {
        [Header("Visual Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private MonoImage bg;
        [SerializeField] private MonoImage searchBar;
        [SerializeField] private MonoOutline searchBarOutline;
        [SerializeField] private MonoTMP_UGUI title;

        [Header("Theme Setting")]
        [HorizontalLine(color: EColor.Gray)]
        [SerializeField] private TMP_Dropdown themeDropdown;
        [SerializeField] private ThemeSO[] themeList;


        private void OnEnable()
        {
            themeDropdown.onValueChanged.AddListener(HandleDropValueChanged);

            themeDropdown.ClearOptions();

            TMP_Dropdown.OptionDataList d = new();
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

            bg.SetColor(theme.BgColor);
            searchBar.SetColor(theme.SearchBarColor);
            searchBarOutline.SetColor(theme.SearchBarOutlineColor);
            title.SetColor(theme.TitleColor);
        }
    }
}
