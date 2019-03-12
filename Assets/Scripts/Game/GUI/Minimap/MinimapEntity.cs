using System;
using UnityEngine;

public class MinimapEntity :MonoBehaviour {
    public Color color;
    public float size;

    private void Start() {
        ManagerManager.GetManager<MinimapManager>().Register(this);
    }

    private void OnDestroy() {
        try {
            ManagerManager.GetManager<MinimapManager>().Unregister(this);
        } catch (NullReferenceException) {

        }
    }
}