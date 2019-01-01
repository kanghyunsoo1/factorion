using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem :MonoBehaviour {
    DataManager dm;
    ResourceManager rm;
    void Start() {
        if (Application.isEditor) {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        dm = GetComponent<DataManager>();
        rm = GetComponent<ResourceManager>();

        Debug.Log("First: " + dm.IsFirst());
        if (dm.IsFirst()) {
            rm.Spawn();
        } else {
            dm.Load();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            dm.Save();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            dm.Load();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            dm.Clean();
        }
    }

}