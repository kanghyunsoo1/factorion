using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuiManager :MonoBehaviour {
    public GameObject saving;
    public GameObject loading;
    public GameObject spawning;
    public Text mapNameText;

    private DataManager _dm;
    void Awake() {
        _dm = GetComponent<DataManager>();
        mapNameText.text = StaticDatas.mapName;
    }


    public void OnSaveStart() {
        saving.SetActive(true);
    }
    public void OnSaveEnd() {
        saving.SetActive(false);
    }
    public void OnLoadStart() {
        loading.SetActive(true);
    }
    public void OnLoadEnd() {
        loading.SetActive(false);
    }
    public void OnSpawnStart() {
        spawning.SetActive(true);
    }
    public void OnSpawnEnd() {
        spawning.SetActive(false);
    }

    public void OnButtonClick(string name) {
        switch (name) {
            case "Save":
                _dm.Save();
                break;
            case "Exit":
                SceneManager.LoadScene("Main");
                break;
            case "Delete":
                _dm.Delete();
                SceneManager.LoadScene("Main");
                break;
        }
    }

    public void RegisterBuildings(GameObject[] buildings) {

    }
}
