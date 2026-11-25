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
        [Tooltip("Overlap in screen pixels when no player has spawned yet.")]
        [SerializeField, Min(1f)] private float roadOverlap = 30f;

        private FuncMapWindow _funcMapWindow;
        private ProgramController _controller;
        private ProgramDragger _dragger;
        private Canvas _canvas;
        private bool _dragStarted;
        private readonly List<(int road, int direction, MapWindowSnapJJW other, int otherRoad)> _links = new();

        private void OnEnable()
        {
            _funcMapWindow = GetComponent<FuncMapWindow>();
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
            for (int i = _links.Count - 1; i >= 0; i--)
            {
                var link = _links[i];
                if (TryGetConnectedEnds(link, out _, out _, out _)) continue;
                if (link.other != null)
                    link.other._links.RemoveAll(reverse => reverse.other == this);
                _links.RemoveAt(i);
            }

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

            float bestDistance = float.PositiveInfinity;
            Vector2 bestOffset = Vector2.zero;
            MapWindowSnapJJW partner = null;
            int myRoad = -1, theirRoad = -1, myDirection = 0;

            foreach (MapWindowSnapJJW other in windows)
            {
                if (other == this
                    || !other.isActiveAndEnabled
                    || other._canvas != _canvas
                    || other._funcMapWindow == null
                    || other._dragger == null
                    || other._dragger.IsDragging)
                {
                    continue;
                }

                Vector2 margin = Vector2.Max(_funcMapWindow.PlayerScreenMargin(), other._funcMapWindow.PlayerScreenMargin());
                float fallback = Mathf.Max(roadOverlap, other.roadOverlap);
                for (int i = 0; i < _funcMapWindow.RoadCount; i++)
                {
                    for (int direction = -1; direction <= 1; direction += 2)
                    {
                        if (!_funcMapWindow.TryGetRoadEnd(i, direction, out MapRoadEnd source))
                            continue;

                        for (int j = 0; j < other._funcMapWindow.RoadCount; j++)
                        {
                            if (!other._funcMapWindow.TryGetRoadEnd(j, -direction, out MapRoadEnd target)
                                || !source.TryGetOffset(target, margin, fallback, snapDistance, out Vector2 offset)
                                || offset.sqrMagnitude >= bestDistance || other.IsEndInUse(j, -direction))
                                continue;

                            bestDistance = offset.sqrMagnitude;
                            bestOffset = offset;
                            partner = other;
                            myRoad = i;
                            theirRoad = j;
                            myDirection = direction;
                        }
                    }
                }
            }

            if (partner != null && ApplySnap(bestOffset))
            {
                _links.Add((myRoad, myDirection, partner, theirRoad));
                partner._links.Add((theirRoad, -myDirection, this, myRoad));
            }
        }

        private bool IsEndInUse(int road, int direction)
        {
            foreach (var link in _links)
                if (link.road == road && link.direction == direction) return true;
            return false;
        }

        private bool TryGetConnectedEnds((int road, int direction, MapWindowSnapJJW other, int otherRoad) link,
            out MapRoadEnd source, out MapRoadEnd target, out Vector2 margin)
        {
            source = target = default;
            margin = default;
            if (link.other == null || !_funcMapWindow.TryGetRoadEnd(link.road, link.direction, out source)
                || !link.other._funcMapWindow.TryGetRoadEnd(link.otherRoad, -link.direction, out target))
                return false;

            margin = Vector2.Max(_funcMapWindow.PlayerScreenMargin(), link.other._funcMapWindow.PlayerScreenMargin());
            float fallback = Mathf.Max(roadOverlap, link.other.roadOverlap);
            return source.TryGetOffset(target, margin, fallback, 1f, out Vector2 offset) && offset.sqrMagnitude <= 1f;
        }

        private bool ApplySnap(Vector2 offset)
        {
            RectTransform panel = _controller.ProgramRect.Rect;
            Vector2 previousPosition = panel.anchoredPosition;

            if (!_funcMapWindow.TryMoveByScreenOffset(offset))
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

            Vector2 screen = _funcMapWindow.MapWorldToScreen(center);
            Vector2 next = _funcMapWindow.MapWorldToScreen(center + step);
            foreach (var link in _links)
            {
                if (!TryGetConnectedEnds(link, out MapRoadEnd source, out MapRoadEnd exit, out Vector2 margin)
                    || (next[source.Axis] - screen[source.Axis]) * link.direction <= 0f
                    || !MapPassageMath.TryCross(source.Area, exit.Area, margin, source.Axis == 0, screen, next, out float fraction))
                    continue;

                FuncMapWindow target = link.other._funcMapWindow;
                Vector2 crossing = Vector2.Lerp(screen, next, fraction);
                if (!target.TryGetLanding(player, crossing, out Vector2 landing)) continue;
                if (fraction > 0f) return fraction;

                _funcMapWindow.TransferPlayerTo(target, landing);
                return 1f;
            }
            return 1f;
        }
    }

    internal readonly struct MapRoadEnd
    {
        public readonly Rect Area;
        public readonly int Axis;
        private readonly int _direction;
        private readonly Vector2 _point;

        public MapRoadEnd(Rect area, Vector2 center, bool horizontal, int direction)
        {
            Area = area;
            Axis = horizontal ? 0 : 1;
            _direction = direction;
            center[Axis] = direction > 0 ? area.max[Axis] : area.min[Axis];
            _point = center;
        }

        public bool TryGetOffset(MapRoadEnd other, Vector2 margin, float fallbackDepth, float snapDistance, out Vector2 offset)
        {
            offset = default;
            if (Axis != other.Axis || _direction != -other._direction) return false;

            float depth = margin[Axis] > 0f ? 2f * margin[Axis] + 2f : fallbackDepth;
            if (Mathf.Min(Area.size[Axis], other.Area.size[Axis]) <= depth + 1f) return false;

            Vector2 delta = other._point - _point;
            float gap = delta[Axis] * _direction;
            if (gap < -depth - 1f) return false;

            int cross = 1 - Axis;
            float separation = Mathf.Max(0f, gap);
            if (separation * separation + delta[cross] * delta[cross] > snapDistance * snapDistance)
                return false;

            float low = Mathf.Max(Area.min[cross] + delta[cross], other.Area.min[cross]);
            float high = Mathf.Min(Area.max[cross] + delta[cross], other.Area.max[cross]);
            if (high <= low || high - low < 2f * margin[cross]) return false;

            offset = delta;
            offset[Axis] += _direction * depth;
            return true;
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
