using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager :MonoBehaviour {
    public float maxTouchDistance;

    private ShowerManager _showerManager;
    private Vector2 _startPos;
    private Camera _camera;
    private AreaManager _areaManager;
    private bool _isOverUI;
    private InventoryGuiManager _inventoryGuiManager;

    private void Awake() {
        _areaManager = GetComponent<AreaManager>();
        _inventoryGuiManager = GetComponent<InventoryGuiManager>();
        _showerManager = GetComponent<ShowerManager>();
        _camera = Camera.main;
    }

    private void Start() {
        StartCoroutine(Touch());
    }

    IEnumerator Touch() {
        yield return null;
        OnWorldTouch(new Vector2(12312412, 1));
    }

    void Update() {
        HandleTouch();
    }

    private void HandleTouch() {
        if (Input.touchCount == 1) {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase) {
                case TouchPhase.Began:
                    _startPos = touch.position;
                    _isOverUI = EventSystem.current.IsPointerOverGameObject(touch.fingerId);
                    break;

                case TouchPhase.Ended:
                    if (Vector2.Distance(_startPos, touch.position) <= maxTouchDistance) {
                        if (!_isOverUI)
                            OnWorldTouch(_camera.ScreenToWorldPoint(touch.position));
                    }
                    break;
            }
        }
    }

    public void OnWorldTouch(Vector2 position) {
        GameObject go = _areaManager.GetUser(position);
        if (go != null) {
            _showerManager.OnTouch(go);
            return;
        } else if (go == null) {
            for (int dx = -1; dx <= 1; dx++) {
                for (int dy = -1; dy <= 1; dy++) {
                    if (dx == 0 && dy == 0)
                        continue;
                    var v = new Vector2(position.x + dx, position.y + dy);
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
