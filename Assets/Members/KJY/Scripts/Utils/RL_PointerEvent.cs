using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Members.KJY.Scripts.Utils
{
    public class RLPointerEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private bool useEnter;
        [ShowIf("useEnter")] 
        public UnityEvent onPointerLeftEnter;       
        [ShowIf("useEnter")] 
        public UnityEvent onPointerRightEnter;

        [SerializeField] private bool useExit;
        [ShowIf("useExit")] 
        public UnityEvent onPointerLeftExit;      
        [ShowIf("useExit")] 
        public UnityEvent onPointerRightExit;

        [SerializeField] private bool useDown;
        [ShowIf("useDown")] 
        public UnityEvent onPointerLeftDown;      
        [ShowIf("useDown")] 
        public UnityEvent onPointerRightDown;

        [SerializeField] private bool useUp;
        [ShowIf("useUp")]
        public UnityEvent onPointerLeftUp;
        [ShowIf("useUp")]
        public UnityEvent onPointerRightUp;

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!useEnter) return;
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    onPointerLeftEnter?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    onPointerRightEnter?.Invoke();
                    break;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!useExit) return;
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    onPointerLeftExit?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    onPointerRightExit?.Invoke();
                    break;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!useDown) return;
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    onPointerLeftDown?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    onPointerRightDown?.Invoke();
                    break;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!useUp) return;
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    onPointerLeftUp?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    onPointerRightUp?.Invoke();
                    break;
            }
        }
    }
}