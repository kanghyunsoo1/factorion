using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager :Manager {
    public float maxTouchDistance;

    private ShowerManager _showerManager;
    private Vector2 _startPos;
    private Camera _camera;
    private AreaManager _areaManager;
    private bool _isOverUI;
    private int _fingerId;
    private InventoryGuiManager _inventoryGuiManager;
    private EventSystem _eventSystem;

    private void Awake() {
        _areaManager = ManagerManager.GetManager<AreaManager>();
        _inventoryGuiManager = ManagerManager.GetManager<InventoryGuiManager>();
        _showerManager = ManagerManager.GetManager<ShowerManager>();
        _camera = Camera.main;
        _eventSystem = EventSystem.current;
    }

    private void Start() {
        StartCoroutine(Touch());
    }

    IEnumerator Touch() {
        yield return null;
        OnWorldTouch(new Vector3(12312412, 1, 1234124));
    }

    void Update() {
        HandleTouch();
    }

    private void HandleTouch() {
        if (Input.touchCount == 1) {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase) {
                case TouchPhase.Began:
                    _fingerId = touch.fingerId;
                    _startPos = touch.position;
                    _isOverUI = _eventSystem.IsPointerOverGameObject(touch.fingerId);
                    break;

                case TouchPhase.Ended:
                    if (_fingerId == touch.fingerId && Vector2.Distance(_startPos, touch.position) <= maxTouchDistance) {
                        if (!_isOverUI) {
                            RaycastHit hit;
                            Physics.Raycast(_camera.ScreenPointToRay(touch.position), out hit);
                            OnWorldTouch(hit.point);
                        }
                    }
                    break;
            }
        }
        if (Input.GetMouseButtonDown(0)) {
            if (!_eventSystem.IsPointerOverGameObject()) {
                RaycastHit hit;
                Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit);
                OnWorldTouch(hit.point);
            }
        }
    }

    public void OnWorldTouch(Vector3 position) {
        GameObject go = _areaManager.GetUser(position);
        if (go != null) {
            _showerManager.OnTouch(go);
            return;
        } else if (go == null) {
            for (int dx = -1; dx <= 1; dx++) {
                for (int dz = -1; dz <= 1; dz++) {
                    if (dx == 0 && dz == 0)
                        continue;
                    var v = new Vector3(position.x + dx, 0, position.z + dz);
                    go = _areaManager.GetUser(v);
                    if (go != null) {
                        _showerManager.OnTouch(go);
                        return;
                    }
                }
            }
        }
        _showerManager.OffAll();
        _inventoryGuiManager.Close();
    }
}
