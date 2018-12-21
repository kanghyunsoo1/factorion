using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager :MonoBehaviour {
    public float maxTouchDistance;//터치로 인정되는 최대 움직인 거리
    private Vector2 startPos;
    private Camera cam;
    private AreaManager am;

    private void Start() {
        am = GetComponent<AreaManager>();
        cam = Camera.main;
    }

    void Update() {
        HandleTouch();
    }

    private void HandleTouch() {
        if (Input.touchCount == 1) {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase) {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    if (Vector2.Distance(startPos, touch.position) <= maxTouchDistance) {
                        OnWorldTouch(cam.ScreenToWorldPoint(touch.position));
                    }
                    break;
            }
        }
    }


    public void OnWorldTouch(Vector2 position) {
        GameObject go = am.GetUser(position);
        if (go != null) {

        }
    }
}
