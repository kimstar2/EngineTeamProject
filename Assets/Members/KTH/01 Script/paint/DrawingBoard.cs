using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawingBoard : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private RawImage _rawImage;

    private Texture2D _texture;
    private RectTransform _rectTransform;

    private Vector2Int _previousPosition;
    private bool _isDrawing;

    private void Awake()
    {
        _rectTransform = _rawImage.rectTransform;

        _texture = new Texture2D(512, 512);
        _texture.filterMode = FilterMode.Point;

        Color[] pixels = new Color[512 * 512];

        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.white;
        }

        _texture.SetPixels(pixels);
        _texture.Apply();

        _rawImage.texture = _texture;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!TryGetPixelPosition(eventData, out _previousPosition)) return;

        _isDrawing = true;
        DrawPixel(_previousPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDrawing) return;

        Draw(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDrawing = false;
    }

    private void Draw(PointerEventData eventData)
    {
        if (!TryGetPixelPosition(eventData, out Vector2Int currentPosition)) return;

        DrawLine(_previousPosition, currentPosition);

        _previousPosition = currentPosition;
        _texture.Apply();
    }

    private bool TryGetPixelPosition(PointerEventData eventData, out Vector2Int pixelPosition)
    {
        pixelPosition = Vector2Int.zero;

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint)) return false;

        Rect rect = _rectTransform.rect;

        float x = (localPoint.x - rect.xMin) / rect.width;
        float y = (localPoint.y - rect.yMin) / rect.height;

        if (x < 0 || x > 1 || y < 0 || y > 1) return false;

        int pixelX = Mathf.Clamp(Mathf.FloorToInt(x * _texture.width), 0, _texture.width - 1);
        int pixelY = Mathf.Clamp(Mathf.FloorToInt(y * _texture.height), 0, _texture.height - 1);

        pixelPosition = new Vector2Int(pixelX, pixelY);

        return true;
    }

    private void DrawPixel(Vector2Int position)
    {
        _texture.SetPixel(position.x, position.y, Color.black);
        _texture.Apply();
    }

    private void DrawLine(Vector2Int start, Vector2Int end)
    {
        int distance = Mathf.CeilToInt(Vector2Int.Distance(start, end));

        for (int i = 0; i <= distance; i++)
        {
            float t = distance == 0 ? 0 : (float)i / distance;

            int x = Mathf.RoundToInt(Mathf.Lerp(start.x, end.x, t));
            int y = Mathf.RoundToInt(Mathf.Lerp(start.y, end.y, t));

            _texture.SetPixel(x, y, Color.black);
        }
    }

    private void OnDestroy()
    {
        if (_texture != null)
        {
            Destroy(_texture);
        }
    }
}
