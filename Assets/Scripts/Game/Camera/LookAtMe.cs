using UnityEngine;
using UnityEngine.EventSystems;

public class LookAtMe :MonoBehaviour {
    private readonly float _zOffset = -6;
    private readonly float _minHeight = 4f, _maxHeight = 60f;
    private readonly float _panSpeed = 0.001f, _zoomSpeed = 0.1f;
    private Camera _camera;
    private float _innerHeight;
    public float height {
        get { return _innerHeight; }
        set {
            _innerHeight = value;
            _camera.transform.position = new Vector3(_camera.transform.position.x, _innerHeight, _camera.transform.position.z);
            var rel = transform.position - _camera.transform.position;
            _camera.transform.rotation = Quaternion.LookRotation(rel);
        }
    }
    private bool _wasZoom;

    private Vector2 _lastPanPosition;
    private Vector2[] _lastZoomPositions;
    private int _panFingerId;
    private EventSystem _eventSystem;

    private void Awake() {
        _camera = Camera.main;
        height = 10f;
        _eventSystem = EventSystem.current;
    }

    private void Update() {
        switch (Input.touchCount) {
            case 1:
                _wasZoom = false;
                Touch touch = Input.GetTouch(0);
                if (_eventSystem.IsPointerOverGameObject(touch.fingerId)) {
                    _lastPanPosition = touch.position;
                    break;
                }
                if (touch.phase == TouchPhase.Began) {
                    _lastPanPosition = touch.position;
                    _panFingerId = touch.fingerId;
                } else if (_panFingerId == touch.fingerId && touch.phase == TouchPhase.Moved) {
                    var delta = _lastPanPosition - touch.position;
                    delta.x = Mathf.Clamp(delta.x, -30f, 30f);
                    delta.y = Mathf.Clamp(delta.y, -30f, 30f);
                    var dx = delta.x * _panSpeed * height;
                    var dy = delta.y * _panSpeed * height;
                    transform.Translate(dx, 0, dy, Space.World);
                    _lastPanPosition = touch.position;
                    var x = Mathf.Clamp(transform.position.x, -StaticDatas.SIZE, StaticDatas.SIZE);
                    var z = Mathf.Clamp(transform.position.z, -StaticDatas.SIZE, StaticDatas.SIZE);
                    transform.position = new Vector3(x, transform.position.y, z);
                }
                break;

            case 2:
                var newPositions = new Vector2[] { Input.GetTouch(0).position, Input.GetTouch(1).position };
                if (!_wasZoom) {
                    _lastZoomPositions = newPositions;
                    _wasZoom = true;
                } else {
                    float newDistance = Vector2.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(_lastZoomPositions[0], _lastZoomPositions[1]);
                    float delta = newDistance - oldDistance;

                    height = Mathf.Clamp(height - delta * _zoomSpeed, _minHeight, _maxHeight);

                    _lastZoomPositions = newPositions;
                }
                break;

            default:
                _wasZoom = false;
                break;
        }


        _camera.transform.position = new Vector3(transform.position.x, height, transform.position.z + _zOffset);
    }

    private void LateUpdate() {
    }
}