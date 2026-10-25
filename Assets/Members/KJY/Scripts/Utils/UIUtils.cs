using UnityEngine;

namespace Members.KJY.Scripts.Utils
{
    public static class UIUtils
    {
        public static void AdjustmentUI(RectTransform parentRect ,RectTransform rect)
        {
            float xMin = parentRect.rect.xMin + rect.rect.width * rect.pivot.x;
            float xMax = parentRect.rect.xMax - rect.rect.width * (1.0f - rect.pivot.x);
            float yMin = parentRect.rect.yMin + rect.rect.height * rect.pivot.y;
            float yMax = parentRect.rect.yMax - rect.rect.height * (1.0f - rect.pivot.y);
            
            rect.anchoredPosition = new Vector2(Mathf.Clamp(rect.anchoredPosition.x, xMin, xMax), Mathf.Clamp(rect.anchoredPosition.y, yMin, yMax));
        }
    }
}