using System;
using UnityEngine;

public class AreaUser :MonoBehaviour {
    private AreaManager _areaManager;

    private void Awake() {
        ManagerManager.SetManagers(this);
    }
    void Start() {
        _areaManager.RegisterUser(gameObject);
    }

    private void OnDestroy() {
        try {
            _areaManager.UnregisterUser(gameObject);
        } catch (Exception) { };
    }
}