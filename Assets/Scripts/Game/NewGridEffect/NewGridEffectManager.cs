using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGridEffectManager :MonoBehaviour {
    public GameObject grid;
    private readonly float _delay = 0.03f;
    private readonly float _lifeTime = 2f;

    private Camera _camera;
    private float _width = 18;
    private float _height = 30;

    private void Awake() {
        _camera = Camera.main;
    }

    void Start() {
        StartCoroutine(Loop());
    }


    IEnumerator Loop() {
        while (true) {
            yield return new WaitForSeconds(_lifeTime * 4);
            var cx = _camera.transform.position.x;
            var cy = _camera.transform.position.y;
            for (float y = _height; y >= -_height; y--) {
                yield return new WaitForSeconds(_delay);
                for (float x = -_width; x <= _width; x++) {

                    var go = (GameObject)Instantiate(grid);
                    go.transform.position = new Vector3(Mathf.Floor(cx + x), Mathf.Floor(cy + y), 0);
                    go.GetComponent<NewGrid>().lifeTime = _lifeTime;
                }
            }
        }
    }
}
