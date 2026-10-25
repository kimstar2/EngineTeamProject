using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Members.JJW._01.Code
{
    public enum ResizeDirection
    {
        Left,
        Right,
        Top,
        Bottom,
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public class ResizeHandle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform panel;
        [SerializeField] private ResizeDirection direction;
        [SerializeField] private Vector2 minSize = new Vector2(80f, 60f);
        [SerializeField] private Camera _mainCamera;

        private bool isResizing = false;
        private RectTransform parentRect;
        private Vector2 dragStartPointer;
        private Vector2 dragStartSize;
        private Vector2 dragStartPosition;


        public void OnBeginDrag(PointerEventData eventData)
        {
            if (panel == null)
            {
                return;
            }

            parentRect = panel.parent as RectTransform;

            if (parentRect == null)
            {
                return;
            }

            bool foundPoint =
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    parentRect,
                    eventData.position,
                    eventData.pressEventCamera,
                    out dragStartPointer
                );

            if (!foundPoint)
            {
                return;
            }

            dragStartSize = panel.rect.size;
            dragStartPosition = panel.anchoredPosition;
            isResizing = true;

            Debug.Log($"드래그 시작 크기: {dragStartSize}");

        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isResizing || panel == null || parentRect == null)
            {
                return;
            }

            bool foundPoint =
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    parentRect,
                    eventData.position,
                    eventData.pressEventCamera,
                    out Vector2 currentPointer
                );

            if (!foundPoint)
            {
                return;
            }

            
            Vector2 movement = currentPointer - dragStartPointer;

            
            float left = 0f;
            float right = dragStartSize.x;
            float bottom = 0f;
            float top = dragStartSize.y;

            
            if (direction == ResizeDirection.Left || direction == ResizeDirection.TopLeft || direction == ResizeDirection.BottomLeft)
            {
                left = Mathf.Min(movement.x, right - minSize.x);
            }

            
            if (direction == ResizeDirection.Right || direction == ResizeDirection.TopRight || direction == ResizeDirection.BottomRight)
            {
                right = Mathf.Max(right + movement.x, left + minSize.x);
            }

            
            if (direction == ResizeDirection.Bottom || direction == ResizeDirection.BottomLeft || direction == ResizeDirection.BottomRight)
            {
                bottom = Mathf.Min(movement.y, top - minSize.y);
            }

            
            if (direction == ResizeDirection.Top || direction == ResizeDirection.TopLeft || direction == ResizeDirection.TopRight)
            {
                top = Mathf.Max(top + movement.y, bottom + minSize.y);
            }

            Vector2 newSize = new Vector2(right - left, top - bottom);

            
            Vector2 positionChange = new Vector2(left, bottom) + Vector2.Scale(newSize - dragStartSize, panel.pivot);

            panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newSize.x);

            panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newSize.y);

            panel.anchoredPosition = dragStartPosition + positionChange;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isResizing = false;
        }
    }
}


