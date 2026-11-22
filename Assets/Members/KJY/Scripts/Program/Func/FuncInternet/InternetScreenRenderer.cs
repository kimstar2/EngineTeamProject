using Members.KJY.Scripts.Utils.Mono;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace Members.KJY.Scripts.Program.Func.FuncInternet
{
    public class InternetScreenRenderer : MonoBehaviour
    {
        [SerializeField] private RectTransform searchScreen;
        public void OnSearch()
        {
            searchScreen.gameObject.SetActive(true);
        }

        public void OffSearch()
        {
            searchScreen.gameObject.SetActive(true);
        }
    }
}
