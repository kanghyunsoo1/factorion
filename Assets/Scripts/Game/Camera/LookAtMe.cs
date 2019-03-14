using UnityEngine;
using UnityEngine.EventSystems;

public class LookAtMe :MonoBehaviour {
    private readonly float _zOffset = -6;
    private readonly float _minHeight = 4f, _maxHeight = 60f;
    private readonly float _panSpeed = 0.001f, _zoomSpeed = 0.1f;
    private Camera _camera;
    private float _innerHeight;
    public float Height {
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
        Height = 10f;
        _eventSystem = EventSystem.current;
    }

    private void Update() {
        Height -= Input.mouseScrollDelta.y * 3;
        transform.Translate(Input.GetAxis("Horizontal") / 2, 0, Input.GetAxis("Vertical") / 2, Space.World);
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
                    var dx = delta.x * _panSpeed * Height;
                    var dy = delta.y * _panSpeed * Height;
                    transform.Translate(dx, 0, dy, Space.World);
                    _lastPanPosition = touch.position;
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

                    Height -= delta * _zoomSpeed;

                    _lastZoomPositions = newPositions;
                }
                break;

            default:
                _wasZoom = false;
                break;
        }

        var x = Mathf.Clamp(transform.position.x, -StaticDatas.SIZE, StaticDatas.SIZE);
        var z = Mathf.Clamp(transform.position.z, -StaticDatas.SIZE, StaticDatas.SIZE);
        transform.position = new Vector3(x, transform.position.y, z);

        _camera.transform.position = new Vector3(transform.position.x, Height, transform.position.z + _zOffset);
        Height = Mathf.Clamp(Height, _minHeight, _maxHeight);
        
    }
    
}