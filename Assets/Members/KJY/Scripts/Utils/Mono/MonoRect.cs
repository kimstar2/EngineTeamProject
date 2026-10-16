using System;
using _TevLib.ServiceLocatorSystem;
using _TevLib.ServiceLocatorSystem.TimeService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Members.KJY.Scripts.Utils.Mono
{
    public class MonoRect : MonoBehaviour
    {
        public RectTransform Rect {get; private set;}

        private void Awake()
        {
            Rect = transform as RectTransform;
        }

        #region Pos
        
        public void SetWorldPos(Vector3 pos)
        {
            pos.z = 0;
            Rect.position = pos;
        }

        public void SetAnchoredPos(Vector3 anchoredPos)
        {
            anchoredPos.z = 0;
            Rect.anchoredPosition = anchoredPos;
        }

        #endregion
        
        #region Size
        
        public void SetSizeDelta(Vector2 size) => Rect.sizeDelta = size;

        public void SetSizeDeltaWithCurrentAnchors(Vector2 delta)
        {
            Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, delta.x);
            Rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, delta.y);
        }
        
        #endregion
        
        #region Anchor
        
        public void SetAnchor(Vector2 minAnchor, Vector2 maxAnchor)
        {
            Rect.anchorMin = minAnchor;
            Rect.anchorMax = maxAnchor;
        }

        public void OnlySetAnchor(Vector2 minAnchor, Vector2 maxAnchor)
        {
            Vector2 originPos = Rect.position;
            Vector2 originDelta = Rect.sizeDelta;
            
            SetAnchor(minAnchor, maxAnchor);
            
            SetWorldPos(originPos);
            SetSizeDelta(originDelta);
        }
        
        #endregion
        
        #region Pivot
        
        public void SetPivot(Vector2 pivot)
        {
            Rect.pivot = pivot;
        }

        /// <summary>
        /// 여기서 피봇 바꾸면 deltaSize의 x,y랑 부호 포함 피봇 차 만큼 곱한걸
        /// 원래 포지션에 더하면 됨
        /// </summary>
        public void OnlySetPivot(Vector2 pivot)
        {
            Vector2 oldPivot = Rect.pivot;
            Vector2 size = Rect.sizeDelta;
            Vector3 oldWorldPosition = Rect.position;

            Vector2 pivotDelta = pivot - oldPivot;

            Rect.pivot = pivot;

            Vector3 localOffset = new Vector3(
                pivotDelta.x * size.x,
                pivotDelta.y * size.y,
                0f
            );

            // 로컬 오프셋을 월드 오프셋으로 변환
            Vector3 worldOffset = Rect.TransformVector(localOffset);

            Rect.position = oldWorldPosition + worldOffset;
        }

        #endregion
        
        #region Extension
        
        public void SetAnchorAndPivot(Vector2 minAnchor, Vector2 maxAnchor, Vector2 pivot)
        {
            SetAnchor(minAnchor, maxAnchor);
            SetPivot(pivot);
        }

        public void OnlySetAnchorAndPivot(Vector2 minAnchor, Vector2 maxAnchor, Vector2 pivot)
        {
            OnlySetAnchor(minAnchor, maxAnchor);
            OnlySetPivot(pivot);
        }

        #endregion
    }
    
}