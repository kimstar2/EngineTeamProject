using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Func;
using UnityEngine;
using UnityEngine.UI;

namespace Members.JJW._01.Code
{
    public class MapWindowFunc : ProgramFunc
    {
        [SerializeField] private GameObject mapWorldPrefab;
        [SerializeField] private RawImage mapView;
        [SerializeField, Min(1f)] private float uiUnitsPerWorldUnit = 100f;

        private static int _nextMapIndex;
        private GameObject _mapWorld;
        private Camera _mapCamera;
        private RenderTexture _renderTexture;
        private PlayerWindowBoundsJJW _playerBounds;
        private ProgramController _programController;
        private RectTransform _panel;
        private Vector2 _acceptedSize;
        private Vector2 _acceptedPosition;
        private bool _hasAcceptedSize;
        
        private MapRoadArea[] _roads;
        
        public int RoadCount => _roads == null ? 0 : _roads.Length;

        public override void Init()
        {
            base.Init();
            if (mapWorldPrefab == null || mapView == null)
            {
                return;
            }

            _programController = GetComponentInParent<ProgramController>();
            if (_programController != null)
                _panel = _programController.ProgramRect.Rect;

            Vector3 mapPosition = new(1000f + _nextMapIndex * 100f, 1000f, 0f);
            _nextMapIndex++;
            _mapWorld = Instantiate(mapWorldPrefab, mapPosition, Quaternion.identity);
            _roads = _mapWorld.GetComponentsInChildren<MapRoadArea>();
            _mapCamera = _mapWorld.GetComponentInChildren<Camera>(true);
            if (_mapCamera == null)
            {
                return;
            }

            _playerBounds = _mapWorld.GetComponentInChildren<PlayerWindowBoundsJJW>();
            if (_playerBounds != null)
                _playerBounds.Initialize(_mapWorld.transform);

            _renderTexture = new RenderTexture(1024, 1024, 24);
            _renderTexture.Create();
            _mapCamera.clearFlags = CameraClearFlags.SolidColor;
            _mapCamera.backgroundColor = Color.black;
            _mapCamera.targetTexture = _renderTexture;
            mapView.texture = _renderTexture;
        }

        private void LateUpdate()
        {
            if (_mapCamera == null || mapView == null) return;

            Canvas.ForceUpdateCanvases();
            Vector2 size = mapView.rectTransform.rect.size;
            if (size.x <= 1f || size.y <= 1f)
            {
                _mapCamera.enabled = false;
                _playerBounds?.Pause();
                return;
            }

            UpdateCamera(size);
            if (_playerBounds == null) return;

            if (IsWindowTransitioning())
            {
                _playerBounds.Pause();
                return;
            }

            if (_playerBounds.TrySetVisibleArea(GetVisibleArea(size)))
            {
                RememberWindowSize();
                return;
            }

            _playerBounds.Pause();
            if (_hasAcceptedSize && _panel != null)
            {
                _panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _acceptedSize.x);
                _panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _acceptedSize.y);
                _panel.anchoredPosition = _acceptedPosition;
                Canvas.ForceUpdateCanvases();
                size = mapView.rectTransform.rect.size;
                UpdateCamera(size);
                if (_playerBounds.TrySetVisibleArea(GetVisibleArea(size))) return;
            }

        }

        private void UpdateCamera(Vector2 size)
        {
            _mapCamera.aspect = size.x / size.y;
            _mapCamera.orthographicSize = size.y / (uiUnitsPerWorldUnit * 2f);
            _mapCamera.enabled = true;
        }

        private Rect GetVisibleArea(Vector2 size)
        {
            Vector2 worldSize = size / uiUnitsPerWorldUnit;
            Vector2 center = _mapCamera.transform.position;
            return new Rect(center - worldSize * 0.5f, worldSize);
        }

        private bool IsWindowTransitioning()
        {
            if (_programController == null) return false;
            ProgramState state = _programController.SystemStateMachine.CurrentProgramState;
            return _programController.IsStateChanging || state == ProgramState.Minimized || state == ProgramState.Closed;
        }

        private void RememberWindowSize()
        {
            if (_panel == null) return;
            _acceptedSize = _panel.rect.size;
            _acceptedPosition = _panel.anchoredPosition;
            _hasAcceptedSize = true;
        }
        
        private Vector2 MapWorldToScreen(Vector2 worldPosition)
        {
            RectTransform viewRect = mapView.rectTransform;
            Rect visibleArea = GetVisibleArea(viewRect.rect.size);
        
            Vector2 normalized = new Vector2(
                (worldPosition.x - visibleArea.xMin) / visibleArea.width,
                (worldPosition.y - visibleArea.yMin) / visibleArea.height
            );
        
            Vector3 localPosition = new Vector3(
                viewRect.rect.xMin + normalized.x * viewRect.rect.width,
                viewRect.rect.yMin + normalized.y * viewRect.rect.height,
                0f
            );
        
            Vector3 uiWorldPosition = viewRect.TransformPoint(localPosition);
        
            Canvas canvas = mapView.canvas;
            Camera uiCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
        
            return RectTransformUtility.WorldToScreenPoint( uiCamera, uiWorldPosition);
        }
        public bool TryGetVisibleRoadRect(int index, out Rect screenRect)
        {
            screenRect = default;

            if (_mapCamera == null
                || mapView == null
                || !isActiveAndEnabled
                || !mapView.isActiveAndEnabled
                || IsWindowTransitioning()
                || index < 0
                || index >= RoadCount)
            {
                return false;
            }

            MapRoadArea road = _roads[index];

            if (road == null || !road.isActiveAndEnabled || road.AreaCollider == null || !road.AreaCollider.enabled)
            {
                return false;
            }

            Vector2 size = mapView.rectTransform.rect.size;

            if (size.x <= 1f || size.y <= 1f) return false;

            Bounds bounds = road.WorldBounds;
            Rect visibleArea = GetVisibleArea(size);

            float left = Mathf.Max(bounds.min.x, visibleArea.xMin);
            float right = Mathf.Min(bounds.max.x, visibleArea.xMax);
            float bottom = Mathf.Max(bounds.min.y, visibleArea.yMin);
            float top = Mathf.Min(bounds.max.y, visibleArea.yMax);

            if (left >= right || bottom >= top) return false;

            Vector2 min = MapWorldToScreen(new Vector2(left, bottom));
            Vector2 max = MapWorldToScreen(new Vector2(right, top));

            screenRect = Rect.MinMaxRect(
                Mathf.Min(min.x, max.x),
                Mathf.Min(min.y, max.y),
                Mathf.Max(min.x, max.x),
                Mathf.Max(min.y, max.y)
            );

            return true;
        }
        public bool TryMoveByScreenOffset(Vector2 screenOffset)
        {
            if (_panel == null
                || mapView == null
                || !isActiveAndEnabled
                || IsWindowTransitioning())
            {
                return false;
            }

            RectTransform parentRect = _panel.parent as RectTransform;
            Canvas canvas = mapView.canvas;

            if (parentRect == null || canvas == null) return false;

            Camera uiCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

            Vector2 currentScreenPosition = RectTransformUtility.WorldToScreenPoint(uiCamera, _panel.position);

            Vector2 targetScreenPosition = currentScreenPosition + screenOffset;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, currentScreenPosition, uiCamera, out Vector2 currentLocal))
            {
                return false;
            }

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, targetScreenPosition, uiCamera, out Vector2 targetLocal))
            {
                return false;
            }

            _panel.anchoredPosition += targetLocal - currentLocal;

            return true;
        }
        public bool TryGetRoadSnapData(int index, out Rect screenRect, out Vector2 center, out bool horizontal)
        {
            center = default;
            horizontal = false;

            if (!TryGetVisibleRoadRect(index, out screenRect)) return false;

            Bounds bounds = _roads[index].WorldBounds;

            center = MapWorldToScreen(bounds.center);
            horizontal = bounds.size.x >= bounds.size.y;

            return true;
        }

        private void OnDestroy()
        {
            if (_mapCamera != null)
            {
                _mapCamera.enabled = false;
                _mapCamera.targetTexture = null;
            }

            if (mapView != null) mapView.texture = null;
            if (_renderTexture != null)
            {
                _renderTexture.Release();
                Destroy(_renderTexture);
            }

            if (_mapWorld != null) Destroy(_mapWorld);
        }
    }
}
