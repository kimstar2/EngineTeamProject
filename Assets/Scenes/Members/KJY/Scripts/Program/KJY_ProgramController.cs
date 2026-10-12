using System;
using Scenes.Members.KJY.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scenes.Members.KJY.Scripts.Program
{
    public class KJY_ProgramController : KJY_ProgramBase
    {
        [field:SerializeField] public RectTransform TargetRect { get; private set; }
        private Vector2 _startPointPos;
        private Vector2 _clickedPointPos;
        private Vector2 _lastPointPos;

        private void Awake()
        {
            DebugLogger.Assert(TargetRect != null,"[ProgramController] TargetRect is null]");
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            _startPointPos = TargetRect.anchoredPosition;
            _clickedPointPos = GetGUIPoint(eventData.position);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            
            if (TargetRect == null) return;
            TargetRect.anchoredPosition = GetGUIPoint(eventData.position) - _clickedPointPos + _startPointPos;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            _lastPointPos = GetGUIPoint(eventData.position);
            Debug.Log("[ProgramController] OnEndDrag()");
        }

        #region Helper

        private static Vector2 GetGUIPoint(Vector2 position) 
            => GUIUtility.ScreenToGUIPoint(position);

        #endregion
    }
}
