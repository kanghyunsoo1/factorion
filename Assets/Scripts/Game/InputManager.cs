using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager :MonoBehaviour {
    public float maxTouchDistance;

    private ShowerManager _sm;
    private Vector2 _startPos;
    private Camera _camera;
    private AreaManager _am;
    private bool _isOverUI;

    private void Awake() {
        _am = GetComponent<AreaManager>();
        _sm = GetComponent<ShowerManager>();
        _camera = Camera.main;
    }

    private void Start() {
        StartCoroutine(Touch());
    }

    IEnumerator Touch() {
        yield return null;
        OnWorldTouch(Vector2.zero);
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
        GameObject go = _am.GetUser(position);
        if (go != null) {
            _sm.OnTouch(go);
            return;
        } else if (go == null) {
            for (int dx = -1; dx <= 1; dx++) {
                for (int dy = -1; dy <= 1; dy++) {
                    if (dx == 0 && dy == 0)
                        continue;
                    var v = new Vector2(position.x + dx, position.y + dy);
                    go = _am.GetUser(v);
                    if (go != null) {
                        _sm.OnTouch(go);
                        return;
                    }
                }
            }
        }
        _sm.OffAll();
        _sm.SelectClear();
    }
}
