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
    public GameObject infoShower;
    public GameObject resourceShower;
    public GameObject select;

    private DataManager _dm;
    private TextManager _tm;
    private ResourceManager _rm;
    private Text _infoNameText;
    private Text _infoDescriptionText;
    private Text _resourceNameText;
    private Text _resourceAmountText;

    void Start() {
        _dm = GetComponent<DataManager>();
        _tm = GetComponent<TextManager>();
        _rm = GetComponent<ResourceManager>();
        mapNameText.text = StaticDatas.mapName;
        _infoNameText = infoShower.transform.Find("NameText").GetComponent<Text>();
        _infoDescriptionText = infoShower.transform.Find("DescriptionText").GetComponent<Text>();
        _resourceNameText = resourceShower.transform.Find("NameText").GetComponent<Text>();
        _resourceAmountText = resourceShower.transform.Find("AmountText").GetComponent<Text>();
    }

    public void OnObjectTouch(GameObject go) {
        InfoShowerOff();
        var res = go.GetComponent<SOResource>();
        if (res != null) {
            resourceShower.SetActive(true);
            _resourceNameText.text = _tm.GetText(_rm.resourceInfos[res.resourceIndex].nameKey);
            _resourceAmountText.text = res.amount + "";
        }

        infoShower.SetActive(true);
        var objName = go.name.Replace("(Clone)", "").ToLower();
        _infoNameText.text = _tm.GetText("name_" + objName);
        _infoDescriptionText.text = _tm.GetText("des_" + objName);
        select.transform.position = go.transform.position;


    }

    public void InfoShowerOff() {
        infoShower.SetActive(false);
        resourceShower.SetActive(false);
        select.transform.position = new Vector3(12354, 12354);
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
            case "Clear":
                _dm.Clean();
                SceneManager.LoadScene("Main");
                break;
        }
    }
}
