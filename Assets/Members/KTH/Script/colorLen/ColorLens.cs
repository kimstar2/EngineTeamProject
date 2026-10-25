
using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ColorLens : MonoBehaviour
{
    public RectTransform Rect { get; private set; }

    public event Action OnLensChanged;

    private Vector3 _lastPosition;
    private Quaternion _lastRotation;
    private Vector2 _lastSize;
    private Vector3 _lastScale;

    private void Awake()
    {
        Rect = GetComponent<RectTransform>();
        CacheTransform();
    }

    private void LateUpdate()
    {
        if (Rect == null)
            return;

        if (_lastPosition != Rect.position ||_lastRotation != Rect.rotation || _lastSize != Rect.rect.size ||_lastScale != Rect.lossyScale)
        {
            CacheTransform();
            OnLensChanged?.Invoke();
        }
    }

    private void CacheTransform()
    {
        _lastPosition = Rect.position;
        _lastRotation = Rect.rotation;
        _lastSize = Rect.rect.size;
        _lastScale = Rect.lossyScale;
    }
}
