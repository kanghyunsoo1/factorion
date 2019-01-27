using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowerManager :MonoBehaviour {

    public GameObject infoShower;
    public GameObject resourceShower;
    public GameObject select;


    private TextManager _tm;
    private ResourceManager _rm;
    private Text _infoNameText;
    private Text _infoDescriptionText;
    private Text _resourceNameText;
    private Text _resourceAmountText;
    void Awake() {

        _tm = GetComponent<TextManager>();
        _rm = GetComponent<ResourceManager>();
        _infoNameText = infoShower.transform.Find("NameText").GetComponent<Text>();
        _infoDescriptionText = infoShower.transform.Find("DescriptionText").GetComponent<Text>();
        _resourceNameText = resourceShower.transform.Find("NameText").GetComponent<Text>();
        _resourceAmountText = resourceShower.transform.Find("AmountText").GetComponent<Text>();
    }
    public void OnObjectTouch(GameObject go) {
        InfoShowerOff();
        var resource = go.GetComponent<Resource>();
        if (resource != null) {
            resourceShower.SetActive(true);
            _resourceNameText.text = _tm.GetText(_rm.resourceInfos[resource.resourceIndex].name);
            _resourceAmountText.text = resource.amount + "";
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
}
