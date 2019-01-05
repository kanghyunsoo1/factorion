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
    public GameObject InfoShower;
    public GameObject ResourceShower;
    public GameObject Select;

    private DataManager dm;
    private TextManager tm;
    private ResourceManager rm;
    private Text infoNameText;
    private Text infoDescriptionText;
    private Text resourceNameText;
    private Text resourceAmountText;

    void Start() {
        dm = GetComponent<DataManager>();
        tm = GetComponent<TextManager>();
        rm = GetComponent<ResourceManager>();
        MapNameText.text = StaticDatas.mapName;
        infoNameText = InfoShower.transform.Find("NameText").GetComponent<Text>();
        infoDescriptionText = InfoShower.transform.Find("DescriptionText").GetComponent<Text>();
        resourceNameText = ResourceShower.transform.Find("NameText").GetComponent<Text>();
        resourceAmountText = ResourceShower.transform.Find("AmountText").GetComponent<Text>();
    }

    public void OnObjectTouch(GameObject go) {
        InfoShowerOff();
        var ih = go.GetComponent<InfoHolder>();
        if (ih != null) {
            InfoShower.SetActive(true);
            infoNameText.text = ih.Name;
            infoDescriptionText.text = ih.Description;
            Select.transform.position = go.transform.position;
            var res = go.GetComponent<Resource>();
            if (res != null) {
                ResourceShower.SetActive(true);
                resourceNameText.text = tm.GetText(rm.GetResourceInfo(res.ResourceId).nameKey);
                resourceAmountText.text = res.Amount + "";
            }
        }
    }

    public void InfoShowerOff() {
        InfoShower.SetActive(false);
        ResourceShower.SetActive(false);
        Select.transform.position = new Vector3(12354, 12354);
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

    public void OnButtonClick(string name) {
        switch (name) {
            case "Save":
                dm.Save();
                break;
            case "Exit":
                SceneManager.LoadScene("Main");
                break;
            case "Clear":
                dm.Clean();
                SceneManager.LoadScene("Main");
                break;
        }
    }
}
