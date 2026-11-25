using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Members.JJW._01.Code
{
    [DefaultExecutionOrder(100)]
    public class MapWindowSnapJJW : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float snapDistance = 30f;
        [SerializeField, Min(1f)] private float roadOverlap = 30f;

        private MapWindow _mapWindow;
        private ProgramController _controller;
        private ProgramDragger _dragger;
        private Canvas _canvas;
        private bool _dragStarted;
        private readonly List<(int road, MapWindowSnapJJW other, int otherRoad)> _links = new();

        private void OnEnable()
        {
            _mapWindow = GetComponent<MapWindow>();
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
            Disconnect();
        }

        private void HandleBeginDragging()
        {
            Disconnect();
            _dragStarted = true;
        }

        private void Disconnect()
        {
            foreach (var link in _links)
                if (link.other != null)
                    link.other._links.RemoveAll(reverse => reverse.other == this);
            _links.Clear();
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

            MapWindowSnapJJW[] windows = FindObjectsByType<MapWindowSnapJJW>();

            bool found = false;
            float bestDistance = snapDistance * snapDistance;
            Vector2 bestOffset = Vector2.zero;
            MapWindowSnapJJW partner = null;
            int myRoad = -1, theirRoad = -1;

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
                    if (!_mapWindow.TryGetRoadSnapData(i, out Rect source, out Vector2 sourceCenter, out bool sourceHorizontal))
                    {
                        continue;
                    }

                    for (int j = 0; j < other._mapWindow.RoadCount; j++)
                    {
                        if (!other._mapWindow.TryGetRoadSnapData(j, out Rect target, out Vector2 targetCenter, out bool targetHorizontal))
                        {
                            continue;
                        }

                        if (sourceHorizontal != targetHorizontal)
                            continue;

                        Vector2 margin = Vector2.Max(_mapWindow.PlayerScreenMargin(), other._mapWindow.PlayerScreenMargin());
                        float overlap = Mathf.Max(roadOverlap, 2f * Mathf.Max(margin.x, margin.y) + 2f);
                        if (!TryCalculateOffset(source, target, sourceCenter, targetCenter, sourceHorizontal, overlap, out Vector2 offset))
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
                        partner = other;
                        myRoad = i;
                        theirRoad = j;
                    }
                }
            }

            if (found && ApplySnap(bestOffset))
            {
                _links.Add((myRoad, partner, theirRoad));
                partner._links.Add((theirRoad, this, myRoad));
            }
        }

        private bool TryCalculateOffset(
            Rect source,
            Rect target,
            Vector2 sourceCenter,
            Vector2 targetCenter,
            bool horizontal,
            float overlap,
            out Vector2 offset)
        {
            offset = Vector2.zero;

            if (source.width < overlap
                || source.height < overlap
                || target.width < overlap
                || target.height < overlap)
            {
                return false;
            }

            if (horizontal)
            {
                offset.y = targetCenter.y - sourceCenter.y;
                offset.x = Mathf.Clamp(0f, target.xMin + overlap - source.xMax, target.xMax - overlap - source.xMin);
            }
            else
            {
                offset.x = targetCenter.x - sourceCenter.x;
                offset.y = Mathf.Clamp(0f, target.yMin + overlap - source.yMax, target.yMax - overlap - source.yMin);
            }

            source.position += offset;

            float overlapWidth = Mathf.Min(source.xMax, target.xMax) - Mathf.Max(source.xMin, target.xMin);

            float overlapHeight = Mathf.Min(source.yMax, target.yMax) - Mathf.Max(source.yMin, target.yMin);

            return overlapWidth >= overlap - 0.01f && overlapHeight >= overlap - 0.01f;
        }

        private bool ApplySnap(Vector2 offset)
        {
            RectTransform panel = _controller.ProgramRect.Rect;
            Vector2 previousPosition = panel.anchoredPosition;

            if (!_mapWindow.TryMoveByScreenOffset(offset))
                return false;

            Vector2 snappedPosition = panel.anchoredPosition;

            UIUtils.AdjustmentUI(_dragger.ProgramData.ParentRect.Rect, panel);

            if ((panel.anchoredPosition - snappedPosition).sqrMagnitude > 0.001f)
            {
                panel.anchoredPosition = previousPosition;
                return false;
            }
            return true;
        }

        internal float WalkAcross(PlayerWindowBoundsJJW player, Vector2 center, Vector2 step)
        {
            if (_links.Count == 0) return 1f;

            Vector2 screen = _mapWindow.MapWorldToScreen(center);
            Vector2 next = _mapWindow.MapWorldToScreen(center + step);
            foreach (var link in _links)
            {
                MapWindow target = link.other._mapWindow;
                if (!_mapWindow.TryGetRoadSnapData(link.road, out Rect a, out Vector2 ca, out bool horizontal)
                    || !target.TryGetRoadSnapData(link.otherRoad, out Rect b, out Vector2 cb, out bool targetHorizontal)
                    || horizontal != targetHorizontal) continue;

                if (Mathf.Abs(horizontal ? ca.y - cb.y : ca.x - cb.x) > 1f) continue;
                
                Vector2 margin = Vector2.Max(_mapWindow.PlayerScreenMargin(), target.PlayerScreenMargin());
                if (!MapPassageMath.TryCross(a, b, margin, horizontal, screen, next, out float fraction))
                    continue;

                Vector2 crossing = Vector2.Lerp(screen, next, fraction);
                if (!target.TryGetLanding(player, crossing, out Vector2 landing)) continue;
                if (fraction > 0f) return fraction;

                _mapWindow.TransferPlayerTo(target, landing);
                return 1f;
            }
            return 1f;
        }
    }

    internal static class MapPassageMath
    {
        public static bool TryCross(Rect source, Rect target, Vector2 margin, bool horizontal,
            Vector2 start, Vector2 end, out float fraction)
        {
            fraction = 0f;
            Vector2 min = Vector2.Max(source.min, target.min) + margin;
            Vector2 max = Vector2.Min(source.max, target.max) - margin;
            
            if (min.x > max.x || min.y > max.y) return false;

            int axis = horizontal ? 0 : 1;
            float travel = end[axis] - start[axis];
            if (Mathf.Abs(travel) < 0.001f) return false;
            if (travel > 0f && target.max[axis] <= source.max[axis] + 0.1f) return false;
            if (travel < 0f && target.min[axis] >= source.min[axis] - 0.1f) return false;

            float seam = (min[axis] + max[axis]) * 0.5f;
            float remaining = (seam - start[axis]) * Mathf.Sign(travel);
            if (remaining > 0.05f)
            {
                fraction = remaining / Mathf.Abs(travel);
                if (fraction > 1f) return false;
            }

            Vector2 point = Vector2.Lerp(start, end, fraction);
            return point.x >= min.x && point.x <= max.x && point.y >= min.y && point.y <= max.y;
        }
    }
}
