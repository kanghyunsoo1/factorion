using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem :MonoBehaviour {
    private DataManager _dm;
    private ResourceManager _rm;
    void Start() {
        if (Application.isEditor) {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        _dm = GetComponent<DataManager>();
        _rm = GetComponent<ResourceManager>();

        Debug.Log("First: " + _dm.IsFirst());
        if (_dm.IsFirst()) {
            _rm.Spawn();
        } else {
            _dm.Load();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            _dm.Save();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            _dm.Load();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            _dm.Clean();
        }
    }

}