using System;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Program.TaskBar;
using Members.KJY.Scripts.Utils;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;
using UnityEngine.EventSystems;
using NotImplementedException = System.NotImplementedException;

namespace Members.KJY.Scripts.Program
{
    public class ProgramDragger : DragBase , IProgramInit
    {
        public ProgramData ProgramData { get; private set; }
        
        private Vector2 _startPointPos;
        private Vector2 _clickedPointPos;
        private RectTransform Rect => ProgramData.ProgramRect.Rect;

        public event Action<ProgramBar> OnBeginDragging;
        
        public void ProgramInit(ProgramData programData) => ProgramData = programData;
        
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            
            OnBeginDragging?.Invoke(ProgramData.ProgramBar);
            _startPointPos = Rect.anchoredPosition;
            _clickedPointPos = GetGUIPoint(eventData.position);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            
            if (Rect == null) return;
            Rect.anchoredPosition = GetGUIPoint(eventData.position) - _clickedPointPos + _startPointPos;
        }

        #region Helper

        private static Vector2 GetGUIPoint(Vector2 position) 
            => GUIUtility.ScreenToGUIPoint(position);

        #endregion
    }
}
