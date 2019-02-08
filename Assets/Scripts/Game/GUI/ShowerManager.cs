using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowerManager :MonoBehaviour {

    public GameObject infoShower;
    public GameObject resourceShower;
    public GameObject select;


    private TextManager _tm;
    private SpriteManager _sm;
    private Text _infoNameText;
    private Text _infoDescriptionText;
    private Text _resourceNameText;
    private Text _resourceAmountText;
    private Image _resourceImage;
    void Awake() {

        _tm = GetComponent<TextManager>();
        _sm = GetComponent<SpriteManager>();
        _infoNameText = infoShower.transform.Find("NameText").GetComponent<Text>();
        _infoDescriptionText = infoShower.transform.Find("DescriptionText").GetComponent<Text>();
        _resourceNameText = resourceShower.transform.Find("NameText").GetComponent<Text>();
        _resourceAmountText = resourceShower.transform.Find("AmountText").GetComponent<Text>();
        _resourceImage = resourceShower.transform.Find("Image").GetComponent<Image>();
    }
    public void OnObjectTouch(GameObject go) {
        InfoShowerOff();
        var resource = go.GetComponent<Resource>();
        if (resource != null) {
            resourceShower.SetActive(true);
            _resourceNameText.text = _tm.GetText(resource.name);
            _resourceAmountText.text = resource.amount + "";
            _resourceImage.sprite = _sm.GetSprite("item", resource.name);
        }


        infoShower.SetActive(true);

        var objName = go.name.Replace("(Clone)", "").ToLower();
        var tName = _tm.GetText("name", objName);
        var tDes = _tm.GetText("des", objName);

        _infoNameText.text = tName;
        _infoDescriptionText.text = tDes;
        select.transform.position = go.transform.position;


    }

    public void InfoShowerOff() {
        infoShower.SetActive(false);
        resourceShower.SetActive(false);
        select.transform.position = new Vector3(12354, 12354);
    }
}
