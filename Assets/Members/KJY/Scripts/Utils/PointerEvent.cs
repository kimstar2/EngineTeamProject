using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Members.KJY.Scripts.Utils
{
    public class PointerEvent : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler , IPointerDownHandler , IPointerUpHandler
    {
        [SerializeField] private bool useEnter;
        [ShowIf("useEnter")] public UnityEvent onPointerEnter;
        
        [SerializeField] private bool useExit;
        [ShowIf("useExit")] public UnityEvent onPointerExit;
        
        [SerializeField] private bool useDown;
        [ShowIf("useDown")] public UnityEvent onPointerDown;
        
        [SerializeField] private bool useUp; 
        [ShowIf("useUp")] public UnityEvent onPointerUp;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (useEnter)
                onPointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (useExit)
                onPointerExit?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (useDown)
                onPointerDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (useUp)
                onPointerUp?.Invoke();
        }
    }
}