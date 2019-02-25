using UnityEngine;

public class CameraHandler :MonoBehaviour {
    private static readonly float _zoomSpeedTouch = 0.05f;
    private static readonly float[] _bounds = new float[] { -StaticDatas.SIZE, StaticDatas.SIZE };
    private static readonly float[] _zoomBounds = new float[] { 6f, 30f };
    private Camera _camera;
    private Vector3 _lastPanPosition;
    private int _panFingerId;
    private bool _wasZoomingLastFrame;
    private Vector2[] _lastZoomPositions;

    void Awake() {
        _camera = GetComponent<Camera>();
    }

    void Update() {
        HandleTouch();
        var v = _camera.transform.position;
        var c = CameraScreenToUnit();
        v.x = Mathf.Clamp(v.x, _bounds[0] + c.x / 2, _bounds[1] - c.x / 2);
        v.y = Mathf.Clamp(v.y, _bounds[0] + c.y / 2, _bounds[1] - c.y / 2);
        _camera.transform.position = v;
    }

    void HandleTouch() {
        switch (Input.touchCount) {
            case 1:
                _wasZoomingLastFrame = false;

                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    _lastPanPosition = touch.position;
                    _panFingerId = touch.fingerId;
                } else if (touch.fingerId == _panFingerId && touch.phase == TouchPhase.Moved) {
                    PanCamera(touch.position);
                }
                break;
            case 2:
                Vector2[] newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!_wasZoomingLastFrame) {
                    _lastZoomPositions = newPositions;
                    _wasZoomingLastFrame = true;
                } else {
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(_lastZoomPositions[0], _lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, _zoomSpeedTouch);

                    _lastZoomPositions = newPositions;
                }
                break;
            default:
                _wasZoomingLastFrame = false;
                break;
        }
    }

    void PanCamera(Vector3 newPanPosition) {
        Vector3 lastScreen = _camera.ScreenToWorldPoint(_lastPanPosition);
        Vector3 newScreen = _camera.ScreenToWorldPoint(newPanPosition);
        Vector3 offset = lastScreen - newScreen;
        transform.Translate(offset, Space.World);
        Vector3 pos = transform.position;
        pos.z = -10f;
        transform.position = pos;
        _lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float offset, float speed) {
        if (offset == 0) {
            return;
        }
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - (offset * speed), _zoomBounds[0], _zoomBounds[1]);
    }
    public Vector2 CameraScreenToUnit() {
        return new Vector2(_camera.orthographicSize * 2 * _camera.aspect, _camera.orthographicSize * 2);
    }
}