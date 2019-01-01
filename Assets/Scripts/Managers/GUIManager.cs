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
    public Image InfoShower;
    public GameObject Select;

    private DataManager dm;
    private TextManager tm;
    private Text infoNameText;
    private Text infoDescriptionText;
    void Start() {
        dm = GetComponent<DataManager>();
        tm = GetComponent<TextManager>();
        MapNameText.text = StaticDatas.mapName;
        infoNameText = InfoShower.transform.Find("NameText").GetComponent<Text>();
        infoDescriptionText = InfoShower.transform.Find("DescriptionText").GetComponent<Text>();
    }

    public void OnObjectTouch(GameObject go) {
        var ih = go.GetComponent<InfoHolder>();
        if (ih != null) {
            InfoShower.gameObject.SetActive(true);
            infoNameText.text = tm.GetText(ih.NameKey);
            infoDescriptionText.text = tm.GetText(ih.NameKey + "_");
            Select.transform.position = go.transform.position;
        }
    }

    public void InfoShowerOff() {
        InfoShower.gameObject.SetActive(false);
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
