using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaUser : MonoBehaviour {
    AreaManager am;
	void Start () {
        am = FindObjectOfType<AreaManager>();
        am.LockArea((int)transform.position.x, (int)transform.position.x);
	}

    private void OnDestroy()
    {
        am.UnlockArea((int)transform.position.x, (int)transform.position.x);
    }
}
