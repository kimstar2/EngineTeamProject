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

        public override void Init()
        {
            base.Init();

            if (mapWorldPrefab == null || mapView == null)
            {
                return;
            }

            Vector3 mapPosition = new Vector3(1000f + _nextMapIndex * 100f, 1000f, 0f);

            _nextMapIndex++;

            _mapWorld = Instantiate(mapWorldPrefab, mapPosition, Quaternion.identity);

            _mapCamera = _mapWorld.GetComponentInChildren<Camera>(true);

            if (_mapCamera == null)
            {
                return;
            }

            _renderTexture = new RenderTexture(1024, 1024, 24);
            _renderTexture.Create();

            _mapCamera.clearFlags = CameraClearFlags.SolidColor;
            _mapCamera.backgroundColor = Color.black;
            _mapCamera.targetTexture = _renderTexture;

            mapView.texture = _renderTexture;
        }

        private void LateUpdate()
        {
            if (_mapCamera == null || mapView == null)
                return;

            Vector2 viewSize = mapView.rectTransform.rect.size;

            if (viewSize.x <= 1f || viewSize.y <= 1f)
            {
                _mapCamera.enabled = false;
                return;
            }

            _mapCamera.aspect = viewSize.x / viewSize.y;
            _mapCamera.orthographicSize = viewSize.y / (uiUnitsPerWorldUnit * 2f);

            _mapCamera.enabled = true;
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

            if (_mapWorld != null)
                Destroy(_mapWorld);
        }
    }
}

