using UnityEngine;

public class ColorLensTarget : MonoBehaviour
{
    private RectTransform _target;
    private RectTransform _revealMask;
    private RectTransform _revealText;
    private ColorLens _colorLens;

    private readonly Vector3[] _targetCorners = new Vector3[4];
    private readonly Vector3[] _lensCorners = new Vector3[4];
    private Vector3 _textWorldPos;

    private void Awake()
    {
        _target = GetComponent<RectTransform>();
        _revealMask = transform.Find("RevealMask").GetComponent<RectTransform>();
        _revealText = _revealMask.Find("RevealText").GetComponent<RectTransform>();
        _colorLens = FindAnyObjectByType<ColorLens>();
    }

    private void Start()
    {
        _textWorldPos = _revealText.position;
        _revealMask.gameObject.SetActive(false);

        if (_colorLens != null)
        {
            _colorLens.OnLensChanged += UpdateReveal;
            UpdateReveal();
        }
    }
    private void UpdateReveal()
    {
        Rect targetRect = GetWorldRect(_target, _targetCorners);
        Rect lensRect = GetWorldRect(_colorLens.Rect, _lensCorners);

        if (!targetRect.Overlaps(lensRect))
        {
            _revealMask.gameObject.SetActive(false);
            return;
        }

        _revealMask.gameObject.SetActive(true);

        float xMin = Mathf.Max(targetRect.xMin, lensRect.xMin);
        float yMin = Mathf.Max(targetRect.yMin, lensRect.yMin);
        float xMax = Mathf.Min(targetRect.xMax, lensRect.xMax);
        float yMax = Mathf.Min(targetRect.yMax, lensRect.yMax);

        Vector3 localMin = _target.InverseTransformPoint(new Vector3(xMin, yMin, _target.position.z));
        Vector3 localMax = _target.InverseTransformPoint(new Vector3(xMax, yMax, _target.position.z));

        _revealMask.anchoredPosition = (localMin + localMax) * 0.5f;
        _revealMask.sizeDelta = new Vector2(Mathf.Abs(localMax.x - localMin.x), Mathf.Abs(localMax.y - localMin.y));
        _revealText.position = _textWorldPos;
    }

    private static Rect GetWorldRect(RectTransform rectTransform, Vector3[] corners)
    {
        rectTransform.GetWorldCorners(corners);
        return Rect.MinMaxRect(corners[0].x, corners[0].y, corners[2].x, corners[2].y);
    }
}