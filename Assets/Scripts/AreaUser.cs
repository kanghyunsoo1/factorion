using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaUser :MonoBehaviour {
    AreaManager am;
    void Start() {
        am = FindObjectOfType<AreaManager>();
        am.LockArea(gameObject);
    }

    private void OnDestroy() {
        am.UnlockArea(gameObject);
    }
}
