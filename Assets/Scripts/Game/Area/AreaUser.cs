using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaUser :MonoBehaviour {
    private AreaManager _am;
    void Start() {
        _am = FindObjectOfType<AreaManager>();
        _am.LockArea(gameObject);
    }

    private void OnDestroy() {
        try {
            _am.UnlockArea(gameObject);
        } catch (Exception) { };
    }
}
