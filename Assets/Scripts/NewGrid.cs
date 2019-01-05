using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGrid :MonoBehaviour {
    public float LifeTime;
    private SpriteRenderer renderer;
    private Color color;
    private bool isUp = true;
    void Start() {
        color = new Color(1f, 1f, 1f, 0f);
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = color;
        Destroy(gameObject, LifeTime);
    }

    void FixedUpdate() {
        if (isUp) {
            color.a += Time.deltaTime / LifeTime * 2;
            if (color.a >= 1f) {
                color.a = 1f;
                isUp = false;
            }
        } else {
            color.a -= Time.deltaTime / LifeTime * 2;
        }

        renderer.color = color;
    }
}
