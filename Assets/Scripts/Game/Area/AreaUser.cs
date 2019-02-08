using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaUser :MonoBehaviour {
    private AreaManager _am;

    private void Awake() {
        _am = FindObjectOfType<AreaManager>();
    }
    void Start() {
        _am.LockArea(gameObject);
    }

    private void OnDestroy() {
        try {
            _am.UnlockArea(gameObject);
        } catch (Exception) { };
    }
}
