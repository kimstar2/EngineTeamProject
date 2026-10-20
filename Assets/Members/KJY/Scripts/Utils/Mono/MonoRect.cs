using System;
using _TevLib.ServiceLocatorSystem;
using _TevLib.ServiceLocatorSystem.TimeService;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Members.KJY.Scripts.Utils.Mono
{
    public class MonoRect : MonoBehaviour
    {
        [field:SerializeField] public RectTransform Rect {get; private set;}

        #region Pos
        
        public void SetWorldPos(Vector2 pos) => Rect.position = pos;
        public void SetAnchoredPos(Vector2 anchoredPos) => Rect.anchoredPosition = anchoredPos;
        
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
        /// 여기서 피봇 바꾸면 rect.size의 x,y랑 부호 포함 피봇 차 만큼 곱한걸
        /// 원래 포지션에 더하면 됨
        /// </summary>
        public void OnlySetPivot(Vector2 pivot)
        {
            Vector2 oldPivot = Rect.pivot;
            Vector2 size = Rect.rect.size; // 스트레치 때문에 size
            Vector3 oldWorldPosition = Rect.position;

            Vector2 pivotDelta = pivot - oldPivot;

            SetPivot(pivot);

            Vector3 localOffset = pivotDelta * size;

            // 로컬 to 월드좌표
            Vector3 worldOffset = Rect.TransformVector(localOffset);
            SetWorldPos( oldWorldPosition + worldOffset);
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
                Vector3 oldWorldPos = Rect.position;
                Vector2 oldSize = Rect.rect.size;
                Vector2 oldPivot = Rect.pivot;

                RectTransform parentRect = Rect.parent as RectTransform;
                if (parentRect == null) return;
                
                SetAnchor(minAnchor, maxAnchor);
                SetPivot(pivot);

                Vector2 anchorRange = maxAnchor - minAnchor;
                Vector2 parentSize = parentRect.rect.size;

                SetSizeDelta(oldSize - parentSize * anchorRange);
                
                Vector2 pivotDelta = (pivot - oldPivot) * oldSize;

                Vector3 worldOffset = Rect.TransformVector(new Vector3(pivotDelta.x, pivotDelta.y, 0f));
                Rect.position = oldWorldPos + worldOffset;
        }

        #endregion
    }
}
