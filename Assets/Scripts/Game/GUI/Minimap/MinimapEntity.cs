using System;
using UnityEngine;

public class MinimapEntity :MonoBehaviour {
    public Color color;
    public float size;
    private MinimapManager _minimapManager;

    private void Start() {
        ManagerManager.SetManagers(this);
        _minimapManager.Register(this);
    }

    private void OnDestroy() {
        try {
            _minimapManager.Unregister(this);
        } catch (NullReferenceException) {

        }
    }
}