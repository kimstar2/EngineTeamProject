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
            return _programController.IsStateChanging
                   || state == ProgramState.Minimized || state == ProgramState.Closed;
        }

        private void RememberWindowSize()
        {
            if (_panel == null) return;
            _acceptedSize = _panel.rect.size;
            _acceptedPosition = _panel.anchoredPosition;
            _hasAcceptedSize = true;
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
