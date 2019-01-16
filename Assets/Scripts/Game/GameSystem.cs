using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem :MonoBehaviour {
    private DataManager _dm;
    private ResourceManager _rm;
    void Awake() {
        _dm = GetComponent<DataManager>();
        _rm = GetComponent<ResourceManager>();
        if (!StaticDatas.wasMainLoad) {
            SceneManager.UnloadSceneAsync("Game");
            SceneManager.LoadScene("Main");
        }

    }

    private void Start() {
        _rm.Spawn();
        _dm.Load();

    }

}