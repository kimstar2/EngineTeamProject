using System.Collections.Generic;
using UnityEngine;

namespace Members.JJW._01.Code
{
    public class PlayerWindowBoundsJJW : MonoBehaviour
    {
        private const float Padding = 0.01f;

        private Rigidbody2D _rb;
        private BoxCollider2D _rbColl;
        private Collider2D[] _obstacles;

        private Rect _centerArea;
        private bool _canMove;

        private readonly List<Rect> _blockedAreas = new();
        private readonly List<float> _candidateX = new();
        private readonly List<float> _candidateY = new();

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rbColl = GetComponent<BoxCollider2D>();
        }

        public void Initialize(Transform mapRoot)
        {
            _obstacles = mapRoot.GetComponentsInChildren<Collider2D>();

            Physics2D.SyncTransforms();

            Pause();
        }

        public void Pause()
        {
            _canMove = false;
            _rb.linearVelocity = Vector2.zero;
        }

        public bool TrySetVisibleArea(Rect visibleArea)
        {
            Vector2 halfSize = _rbColl.bounds.extents;

            if (!WindowBoundsMathJJW.TryGetCenterArea(
                    visibleArea, halfSize, Padding, out Rect area))
            {
                return false;
            }

            Vector2 offset = _rb.transform.TransformVector(_rbColl.offset);

            Vector2 center = _rb.position + offset;

            if (!WindowBoundsMathJJW.Contains(area, center))
            {
                CollectBlockedAreas(halfSize);

                if (!WindowBoundsMathJJW.TryFindNearest(
                        area,
                        center,
                        _blockedAreas,
                        _candidateX,
                        _candidateY,
                        out Vector2 target))
                {
                    return false;
                }

                _rb.position = target - offset;
                _rb.linearVelocity = Vector2.zero;
            }

            _centerArea = area;
            _canMove = true;

            return true;
        }

        public Vector2 LimitVelocity(Vector2 velocity, float deltaTime)
        {
            if (!_canMove || deltaTime <= 0f)
                return Vector2.zero;

            Vector2 offset = _rb.transform.TransformVector(_rbColl.offset);

            Vector2 center = _rb.position + offset;

            if (!WindowBoundsMathJJW.Contains(_centerArea, center))
                return Vector2.zero;

            Vector2 next = center + velocity * deltaTime;
            Vector2 limited = WindowBoundsMathJJW.Clamp(_centerArea, next);

            return (limited - center) / deltaTime;
        }

        private void CollectBlockedAreas(Vector2 halfSize)
        {
            _blockedAreas.Clear();

            if (_obstacles == null)
                return;

            foreach (Collider2D obstacle in _obstacles)
            {
                if (obstacle == null
                    || !obstacle.enabled
                    || obstacle.isTrigger
                    || !obstacle.gameObject.activeInHierarchy
                    || obstacle.attachedRigidbody == _rb)
                {
                    continue;
                }

                if (Physics2D.GetIgnoreLayerCollision(
                        gameObject.layer, obstacle.gameObject.layer)
                    || Physics2D.GetIgnoreCollision(_rbColl, obstacle))
                {
                    continue;
                }

                Bounds bounds = obstacle.bounds;

                _blockedAreas.Add(Rect.MinMaxRect(
                    bounds.min.x - halfSize.x - Padding,
                    bounds.min.y - halfSize.y - Padding,
                    bounds.max.x + halfSize.x + Padding,
                    bounds.max.y + halfSize.y + Padding));
            }
        }
    }

    internal static class WindowBoundsMathJJW
    {
        public static bool TryGetCenterArea(Rect visible, Vector2 halfSize, float padding, out Rect area)
        {
            float left = visible.xMin + halfSize.x + padding;
            float right = visible.xMax - halfSize.x - padding;
            float bottom = visible.yMin + halfSize.y + padding;
            float top = visible.yMax - halfSize.y - padding;

            area = Rect.MinMaxRect(left, bottom, right, top);

            return left <= right && bottom <= top;
        }

        public static bool Contains(Rect area, Vector2 point)
        {
            return point.x >= area.xMin
                   && point.x <= area.xMax
                   && point.y >= area.yMin
                   && point.y <= area.yMax;
        }

        public static Vector2 Clamp(Rect area, Vector2 point)
        {
            return new Vector2(Mathf.Clamp(point.x, area.xMin, area.xMax), 
                Mathf.Clamp(point.y, area.yMin, area.yMax));
        }

        private static bool IsFree(Vector2 point, List<Rect> blocked)
        {
            foreach (Rect wall in blocked)
            {
                if (point.x > wall.xMin
                    && point.x < wall.xMax
                    && point.y > wall.yMin
                    && point.y < wall.yMax)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TryFindNearest(
            Rect area,
            Vector2 origin,
            List<Rect> blocked,
            List<float> candidateX,
            List<float> candidateY,
            out Vector2 target)
        {
            target = Clamp(area, origin);

            if (IsFree(target, blocked))
                return true;

            candidateX.Clear();
            candidateY.Clear();

            candidateX.Add(target.x);
            candidateX.Add(area.xMin);
            candidateX.Add(area.xMax);

            candidateY.Add(target.y);
            candidateY.Add(area.yMin);
            candidateY.Add(area.yMax);

            foreach (Rect wall in blocked)
            {
                candidateX.Add(Mathf.Clamp(wall.xMin, area.xMin, area.xMax));

                candidateX.Add(Mathf.Clamp(wall.xMax, area.xMin, area.xMax));

                candidateY.Add(Mathf.Clamp(wall.yMin, area.yMin, area.yMax));

                candidateY.Add(Mathf.Clamp(wall.yMax, area.yMin, area.yMax));
            }

            bool found = false;
            float bestDistance = float.PositiveInfinity;

            foreach (float x in candidateX)
            {
                foreach (float y in candidateY)
                {
                    Vector2 candidate = new Vector2(x, y);

                    if (!IsFree(candidate, blocked))
                        continue;

                    float distance = (candidate - origin).sqrMagnitude;

                    if (distance >= bestDistance)
                        continue;

                    target = candidate;
                    bestDistance = distance;
                    found = true;
                }
            }
            return found;
        }
    }
}