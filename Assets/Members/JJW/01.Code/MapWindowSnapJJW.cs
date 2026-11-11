using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Utils;
using UnityEngine;

namespace Members.JJW._01.Code
{
    [DefaultExecutionOrder(100)]
    public class MapWindowSnapJJW : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float snapDistance = 30f;
        [SerializeField, Min(1f)] private float roadOverlap = 30f;

        private MapWindowFunc _mapWindow;
        private ProgramController _controller;
        private ProgramDragger _dragger;
        private Canvas _canvas;
        private bool _dragStarted;

        private void OnEnable()
        {
            _mapWindow = GetComponent<MapWindowFunc>();
            _controller = GetComponentInParent<ProgramController>();

            Canvas canvas = GetComponentInParent<Canvas>();
            _canvas = canvas == null ? null : canvas.rootCanvas;

            if (_controller == null)
                return;

            _dragger = _controller.ProgramDragger;

            if (_dragger != null)
                _dragger.OnBeginDragging += HandleBeginDragging;
        }

        private void OnDisable()
        {
            if (_dragger != null)
                _dragger.OnBeginDragging -= HandleBeginDragging;

            _dragStarted = false;
        }

        private void HandleBeginDragging()
        {
            _dragStarted = true;
        }

        private void LateUpdate()
        {
            if (!_dragStarted || _dragger == null || _dragger.IsDragging)
                return;

            _dragStarted = false;
            TrySnap();
        }

        private void TrySnap()
        {
            if (_canvas == null || _dragger.ProgramData == null)
                return;

            Canvas.ForceUpdateCanvases();

            MapWindowSnapJJW[] windows =
                FindObjectsByType<MapWindowSnapJJW>(
                    FindObjectsSortMode.InstanceID);

            bool found = false;
            float bestDistance = snapDistance * snapDistance;
            Vector2 bestOffset = Vector2.zero;

            foreach (MapWindowSnapJJW other in windows)
            {
                if (other == this
                    || !other.isActiveAndEnabled
                    || other._canvas != _canvas
                    || other._mapWindow == null
                    || other._dragger == null
                    || other._dragger.IsDragging)
                {
                    continue;
                }

                for (int i = 0; i < _mapWindow.RoadCount; i++)
                {
                    if (!_mapWindow.TryGetRoadSnapData(
                            i, out Rect source, out Vector2 sourceCenter,
                            out bool sourceHorizontal))
                    {
                        continue;
                    }

                    for (int j = 0; j < other._mapWindow.RoadCount; j++)
                    {
                        if (!other._mapWindow.TryGetRoadSnapData(
                                j, out Rect target, out Vector2 targetCenter,
                                out bool targetHorizontal))
                        {
                            continue;
                        }

                        if (sourceHorizontal != targetHorizontal)
                            continue;

                        if (!TryCalculateOffset(
                                source, target, sourceCenter, targetCenter,
                                sourceHorizontal, out Vector2 offset))
                        {
                            continue;
                        }

                        float distance = offset.sqrMagnitude;

                        if (distance > bestDistance)
                            continue;

                        if (found && distance >= bestDistance)
                            continue;

                        found = true;
                        bestDistance = distance;
                        bestOffset = offset;
                    }
                }
            }

            if (found)
                ApplySnap(bestOffset);
        }

        private bool TryCalculateOffset(
            Rect source,
            Rect target,
            Vector2 sourceCenter,
            Vector2 targetCenter,
            bool horizontal,
            out Vector2 offset)
        {
            offset = Vector2.zero;

            if (source.width < roadOverlap
                || source.height < roadOverlap
                || target.width < roadOverlap
                || target.height < roadOverlap)
            {
                return false;
            }

            if (horizontal)
            {
                offset.y = targetCenter.y - sourceCenter.y;
                offset.x = Mathf.Clamp(
                    0f,
                    target.xMin + roadOverlap - source.xMax,
                    target.xMax - roadOverlap - source.xMin);
            }
            else
            {
                offset.x = targetCenter.x - sourceCenter.x;
                offset.y = Mathf.Clamp(
                    0f,
                    target.yMin + roadOverlap - source.yMax,
                    target.yMax - roadOverlap - source.yMin);
            }

            source.position += offset;

            float overlapWidth =
                Mathf.Min(source.xMax, target.xMax)
                - Mathf.Max(source.xMin, target.xMin);

            float overlapHeight =
                Mathf.Min(source.yMax, target.yMax)
                - Mathf.Max(source.yMin, target.yMin);

            return overlapWidth >= roadOverlap - 0.01f
                   && overlapHeight >= roadOverlap - 0.01f;
        }

        private void ApplySnap(Vector2 offset)
        {
            RectTransform panel = _controller.ProgramRect.Rect;
            Vector2 previousPosition = panel.anchoredPosition;

            if (!_mapWindow.TryMoveByScreenOffset(offset))
                return;

            Vector2 snappedPosition = panel.anchoredPosition;

            UIUtils.AdjustmentUI(
                _dragger.ProgramData.ParentRect.Rect, panel);

            if ((panel.anchoredPosition - snappedPosition).sqrMagnitude > 0.001f)
                panel.anchoredPosition = previousPosition;
        }
    }
}