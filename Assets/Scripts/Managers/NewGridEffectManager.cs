using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGridEffectManager :MonoBehaviour {
    public GameObject Grid;
    public float Delay;
    public float LifeTime;

    private Camera cam;
    private float width = 18;
    private float height = 30;
    private bool isAdd = false;
    void Start() {
        cam = Camera.main;
        StartCoroutine(Loop());
    }


    IEnumerator Loop() {
        while (true) {
            yield return new WaitForSeconds(LifeTime * 4);
            var cx = cam.transform.position.x;
            var cy = cam.transform.position.y;
            for (float y = height; y >= -height; y--) {
                yield return new WaitForSeconds(Delay);
                for (float x = -width; x <= width; x++) {

                    var go = (GameObject)Instantiate(Grid);
                    go.transform.position = new Vector3(Mathf.Floor(cx + x), Mathf.Floor(cy + y), 0);
                    go.GetComponent<NewGrid>().LifeTime = LifeTime;
                }
            }
        }
    }
}
