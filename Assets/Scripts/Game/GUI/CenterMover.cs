using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterMover :MonoBehaviour {
    private Transform _cam;

    private void Awake() {
        _cam = Camera.main.transform;
    }


    void Update() {
        transform.position = new Vector3(Mathf.FloorToInt(_cam.position.x), Mathf.FloorToInt(_cam.position.y)+1, 0);

    }
}
