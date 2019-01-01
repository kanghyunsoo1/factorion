using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager :MonoBehaviour {
    public float MaxTouchDistance;
    private GUIManager guim;
    private Vector2 startPos;
    private Camera cam;
    private AreaManager am;

    private void Start() {
        am = GetComponent<AreaManager>();
        guim = GetComponent<GUIManager>();
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
                    if (Vector2.Distance(startPos, touch.position) <= MaxTouchDistance) {
                        if(!EventSystem.current.IsPointerOverGameObject())
                        OnWorldTouch(cam.ScreenToWorldPoint(touch.position));
                    }
                    break;
            }
        }
    }


    public void OnWorldTouch(Vector2 position) {
        GameObject go = am.GetUser(position);
        if (go != null) {
            guim.OnObjectTouch(go);
        }
    }
}
