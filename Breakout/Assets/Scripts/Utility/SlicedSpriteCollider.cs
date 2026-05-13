using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class SlicedSpriteCollider : MonoBehaviour
{
    private BoxCollider2D _collider;
    private SpriteRenderer _renderer;

    private Vector2 _lastSize;

    private void Awake()
    {
        Cache();
        UpdateCollider();
    }

    private void Cache()
    {
        if (_collider == null) _collider = GetComponent<BoxCollider2D>();
        if (_renderer == null) _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_renderer.size != _lastSize)
        {
            Cache();
            UpdateCollider();
        }
    }

    private void UpdateCollider()
    {
        _lastSize = _renderer.size;
        _collider.size = _renderer.size;
    }
}
