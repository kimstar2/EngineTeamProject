using System;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.Program.TaskBar;
using Members.KJY.Scripts.Utils;
using Members.KJY.Scripts.Utils.Mono;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Members.KJY.Scripts.Program
{
    public class ProgramDragger : DragBase , IProgramInit
    {
        [SerializeField] private bool rangeAdjustmentOnEndDrag;
        private Vector2 _startPointPos;
        private Vector2 _clickedPointPos;
        private RectTransform Rect => ProgramData.ProgramRect.Rect;

        public ProgramData ProgramData { get; private set; }
        public event Action OnBeginDragging;
        
        public void ProgramInit(ProgramData programData) => ProgramData = programData;
        
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            
            OnBeginDragging?.Invoke();
            _startPointPos = Rect.anchoredPosition;
            _clickedPointPos = GetGUIPoint(eventData.position);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            
            if (Rect == null) return;
            
            Rect.anchoredPosition = GetGUIPoint(eventData.position) - _clickedPointPos + _startPointPos;
            
            if (!rangeAdjustmentOnEndDrag)
                UIUtils.AdjustmentUI(ProgramData.ParentRect.Rect,Rect);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            if (rangeAdjustmentOnEndDrag)
                UIUtils.AdjustmentUI(ProgramData.ParentRect.Rect,Rect);
        }

        #region Helper

        private static Vector2 GetGUIPoint(Vector2 position) 
            => GUIUtility.ScreenToGUIPoint(position);

        #endregion
    }
}
