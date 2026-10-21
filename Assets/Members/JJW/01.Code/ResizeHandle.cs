using UnityEngine;
using UnityEngine.EventSystems;

namespace Members.JJW._01.Code
{
    public class ResizeHandle : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("크기 조절 ㄹㅊㄱ");
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log($"마우스 이동량:{eventData.delta}");
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("크기 조질 끝");
        }
    }
}


