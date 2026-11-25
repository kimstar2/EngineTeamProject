using _TevLib.ServiceLocatorSystem;
using Members.KJY.Scripts.Core.Services;
using Members.KJY.Scripts.Program;
using Members.KJY.Scripts.Program.Entry;
using Members.KJY.Scripts.Program.Error;
using Members.KJY.Scripts.Program.Func;
using Members.KJY.Scripts.Program.System;
using Members.KJY.Scripts.World;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Members.JJW._01.Code
{
    public class FuncMapWindow : ProgramFunc
    {
        [SerializeField] private RawImage mapView;
        [SerializeField, Min(1f)] private float uiUnitsPerWorldUnit = 100f;

        public ProgramController PC { get; private set; }
            
        private static int _nextMapIndex;
        private WorldMap _worldMap;
        private RenderTexture _renderTexture;
        private PlayerWindowBoundsJJW _playerBounds;
        private RectTransform _panel;
        private Vector2 _acceptedSize;
        private Vector2 _acceptedPosition;
        private bool _hasAcceptedSize;
        
        private MapRoadArea[] _roads;
        private Collider2D[] _mapColliders;
        internal MapWindowSnapJJW Snap { get; private set; }
        private static PlayerWindowBoundsJJW _activePlayer;
        
        private IMapManager _mapManager;
        
        public int RoadCount => _roads == null ? 0 : _roads.Length;
        
        public void InitManager()
        {
            _mapManager = ServiceLocator.GetService<IMapManager>();
        }

        public void MakeWorldMap(MapEntry mapEntry , ProgramController pC)
        {
            PC = pC;
            Vector3 mapPosition = new(1000f + _nextMapIndex * 100f, 1000f, 0f);

            if (_mapManager.TryGet(mapEntry, out var worldMap))
                _worldMap = worldMap;
            else
            {
                _worldMap = Instantiate(mapEntry.WorldMap, mapPosition, Quaternion.identity);
                _mapManager.Add(mapEntry,_worldMap);
            }
            _worldMap.SetActive(true);

            if (PC != null)
                _panel = PC.ProgramRect.Rect;

            if (_worldMap.MapCamera == null)
                return;
            
            _nextMapIndex++;
            _roads = _worldMap.Loads;
            _mapColliders = _worldMap.MapColliders;

            Snap = GetComponent<MapWindowSnapJJW>();

            var spawnPlayer = _worldMap.GetComponentInChildren<PlayerWindowBoundsJJW>();
            if (spawnPlayer != null)
            {
                if (_mapManager.ActivePlayer == null)
                {
                    _activePlayer = _playerBounds = spawnPlayer;
                    _playerBounds.Initialize(this, _mapColliders);
                }
                else
                {
                    spawnPlayer.gameObject.SetActive(false);
                    Destroy(spawnPlayer.gameObject);
                }
            }

            
            if (!_worldMap.IsSetTexture)
            {
                _renderTexture = new RenderTexture(1024, 1024, 24);
                _renderTexture.Create();
                _worldMap.MapCamera.clearFlags = CameraClearFlags.SolidColor;
                _worldMap.MapCamera.backgroundColor = Color.black;
                _worldMap.MapCamera.targetTexture = _renderTexture;
                mapView.texture = _renderTexture;
                _worldMap.SetTexture(_renderTexture);
            }
            else
                mapView.texture = _worldMap.CameraTexture;
        }

        private void LateUpdate()
        {
            Canvas.ForceUpdateCanvases();
            Vector2 size = mapView.rectTransform.rect.size;
            if (size.x <= 1f || size.y <= 1f)
            {
                _worldMap.MapCamera.enabled = false;
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
            _worldMap.MapCamera.aspect = size.x / size.y;
            _worldMap.MapCamera.orthographicSize = size.y / (uiUnitsPerWorldUnit * 2f);
            _worldMap.MapCamera.enabled = true;
        }

        private Rect GetVisibleArea(Vector2 size)
        {
            Vector2 worldSize = size / uiUnitsPerWorldUnit;
            Vector2 center = _worldMap.MapCamera.transform.position;
            return new Rect(center - worldSize * 0.5f, worldSize);
        }

        private bool IsWindowTransitioning()
        {
            if (PC == null) return false;
            ProgramState state = PC.SystemStateMachine.CurrentProgramState;
            return PC.IsStateChanging || state == ProgramState.Minimized || state == ProgramState.Closed;
        }

        private void RememberWindowSize()
        {
            if (_panel == null) return;
            _acceptedSize = _panel.rect.size;
            _acceptedPosition = _panel.anchoredPosition;
            _hasAcceptedSize = true;
        }
        
        internal Vector2 MapWorldToScreen(Vector2 worldPosition)
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

            if (_worldMap.MapCamera == null
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

        internal Vector2 PlayerScreenMargin()
        {
            if (_activePlayer == null) return Vector2.zero;
            Vector2 origin = _worldMap.MapCamera.transform.position;
            return MapWorldToScreen(origin + _activePlayer.Margin) - MapWorldToScreen(origin);
        }

        internal bool TryGetLanding(PlayerWindowBoundsJJW player, Vector2 screen, out Vector2 center)
        {
            center = default;
            if (_playerBounds != null) return false;
            RectTransform view = mapView.rectTransform;
            Canvas canvas = mapView.canvas.rootCanvas;
            Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(view, screen, cam, out Vector2 local))
                return false;

            Rect visible = GetVisibleArea(view.rect.size);
            center = Rect.NormalizedToPoint(visible, Rect.PointToNormalized(view.rect, local));
            return player.CanStandAt(center, visible, _mapColliders);
        }

        internal void TransferPlayerTo(FuncMapWindow destination, Vector2 center)
        {
            var player = _playerBounds;
            _playerBounds = null;
            destination._playerBounds = player;
            player.transform.SetParent(destination._worldMap.transform, true);
            player.Initialize(destination, destination._mapColliders);
            player.PlaceAt(center);
            destination._playerBounds.TrySetVisibleArea(destination.GetVisibleArea(destination.mapView.rectTransform.rect.size));
            destination.RememberWindowSize();
            if (destination.PC != null)
                destination.PC.ChangeState(SystemInvoker.ProgramController, ProgramState.Enabled);
        }

        public void StateEnable()
        {
            PC.ChangeState(SystemInvoker.ProgramController, ProgramState.Enabled);
        }
    }
}
