using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager :MonoBehaviour {
    public GameObject Saving;
    public GameObject Loading;
    public GameObject Spawning;
    public Text MapNameText;
    DataManager dm;
    void Start() {
        dm = GetComponent<DataManager>();
        MapNameText.text = StaticDatas.mapName;
    }

    public void OnObjectTouch(GameObject go) {
        if (go.GetComponent<Resource>() != null) {

        }
    }


    public void OnSaveStart() {
        Saving.SetActive(true);
    }
    public void OnSaveEnd() {
        Saving.SetActive(false);
    }
    public void OnLoadStart() {
        Loading.SetActive(true);
    }
    public void OnLoadEnd() {
        Loading.SetActive(false);
    }
    public void OnSpawnStart() {
        Spawning.SetActive(true);
    }
    public void OnSpawnEnd() {
        Spawning.SetActive(false);
    }
    public void OnSave() {
        dm.Save();
    }
    public void OnExit() {
        SceneManager.LoadScene("Main");
    }
}
