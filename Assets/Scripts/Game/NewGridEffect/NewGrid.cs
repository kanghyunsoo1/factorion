using UnityEngine;

public class NewGrid :MonoBehaviour {
    public float lifeTime;

    private SpriteRenderer _spriteRenderer;
    private Color _color;
    private bool _isUp = true;

    void Start() {
        _color = new Color(1f, 1f, 1f, 0f);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = _color;
        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate() {
        if (_isUp) {
            _color.a += Time.deltaTime / lifeTime * 2;
            if (_color.a >= 1f) {
                _color.a = 1f;
                _isUp = false;
            }
        } else {
            _color.a -= Time.deltaTime / lifeTime * 2;
        }
        _spriteRenderer.color = _color;
    }
}