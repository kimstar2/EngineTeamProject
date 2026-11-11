using System;
using Members.KJY.Scripts.Utils;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program.Func.FuncFileExplorer
{
    public class FilePath : MonoBehaviour , IPointerDownHandler , IPointerUpHandler
    {
        [SerializeField] private MonoTMP_UGUI path;
        private bool _canClick;
        private int _myIndex;
        
        public event Action<int> OnClick;

        public void SetCanClick(bool canClick)
        {
            _canClick = canClick;
        }

        public void SetMyNum(int myIndex)
        {
            _myIndex = myIndex;
        }
        
        public void SetPath(string t)
        {
            path.SetText(t);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_canClick) return;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_canClick) return;
            OnClick?.Invoke(_myIndex);
        }
    }
}