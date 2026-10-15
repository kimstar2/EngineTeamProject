using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.Members.KJY.Scripts.Program
{
    public abstract class KJY_ProgramBase : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public bool IsDragging {get; protected set;}

        public virtual void OnDrag(PointerEventData eventData)
        {
            IsDragging = true;
        }

        public virtual void OnBeginDrag(PointerEventData eventData) { }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            IsDragging = false;
        }
    }
}