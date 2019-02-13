using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapEntity :MonoBehaviour {
    public Color color;
    public float size;

    private void Start() {
        FindObjectOfType<MinimapManager>().Register(this);
    }

    private void OnDestroy() {
        try {
            FindObjectOfType<MinimapManager>().Unregister(this);
        } catch (NullReferenceException) {

        }
    }
}
