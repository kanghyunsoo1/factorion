using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem :MonoBehaviour {
    DataManager dm;
    ResourceManager rm;
    void Start() {
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
            Debug.Log("Clean");
            dm.Clean();
        }
    }

}