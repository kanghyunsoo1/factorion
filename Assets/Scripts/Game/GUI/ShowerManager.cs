using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowerManager :MonoBehaviour {

    public GameObject infoShower;
    public GameObject resourceShower;
    public GameObject select;
    public GameObject inventoryShower;
    public GameObject inventorySlot;


    private TextManager _tm;
    private SpriteManager _sm;
    private Text _infoNameText;
    private Text _infoDescriptionText;
    private Text _resourceNameText;
    private Text _resourceAmountText;
    private Image _resourceImage;
    private ShowerInventorySlot[] _slots;
    private GameObject _slotButton;
    private GameObject _select;
    private Inventory _inventory;

    void Awake() {
        _tm = GetComponent<TextManager>();
        _sm = GetComponent<SpriteManager>();
        _infoNameText = infoShower.transform.Find("NameText").GetComponent<Text>();
        _infoDescriptionText = infoShower.transform.Find("DescriptionText").GetComponent<Text>();
        _resourceNameText = resourceShower.transform.Find("NameText").GetComponent<Text>();
        _resourceAmountText = resourceShower.transform.Find("AmountText").GetComponent<Text>();
        _resourceImage = resourceShower.transform.Find("Image").GetComponent<Image>();
        _slots = new ShowerInventorySlot[] {
            Instantiate(inventorySlot).GetComponent<ShowerInventorySlot>()
            ,Instantiate(inventorySlot).GetComponent<ShowerInventorySlot>()
            ,Instantiate(inventorySlot).GetComponent<ShowerInventorySlot>()
            ,Instantiate(inventorySlot).GetComponent<ShowerInventorySlot>()
        };
        for (int i = 0; i < 4; i++) {
            _slots[i].transform.SetParent(inventoryShower.transform);
            _slots[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 50, 0);
            _slots[i].gameObject.SetActive(false);
        }
        _slotButton = inventoryShower.transform.Find("Button").gameObject;
    }
    private void Start() {
        StartCoroutine(Refresh());
    }

    public void OnTouch(GameObject go) {
        if (go == null)
            return;
        _select = go;
        OffAll();
        var resource = go.GetComponent<Resource>();
        _inventory = go.GetComponent<Inventory>();
        if (resource != null) {
            resourceShower.SetActive(true);
            _resourceNameText.text = _tm.GetText("item", resource.name);
            _resourceAmountText.text = resource.amount + "";
            _resourceImage.sprite = _sm.GetSprite("item", resource.name);
        }

        if (_inventory != null) {
            inventoryShower.SetActive(true);
            var stacks = _inventory.GetStacks();
            int max = stacks.Length;
            if (max > 4) {
                _slotButton.SetActive(true);
                max = 4;
            }
            for (int i = 0; i < max; i++) {
                _slots[i].gameObject.SetActive(true);
                _slots[i].SetItem(_sm.GetSprite("item", stacks[i].name), stacks[i].count);
            }
        }

        infoShower.SetActive(true);

        var objName = go.name.Replace("(Clone)", "").ToLower();
        var tName = _tm.GetText("name", objName);
        var tDes = _tm.GetText("des", objName);

        _infoNameText.text = tName;
        _infoDescriptionText.text = tDes;
        select.transform.position = go.transform.position;

    }

    public void OffAll() {
        infoShower.SetActive(false);
        inventoryShower.SetActive(false);
        try {
            foreach (var i in _slots)
                i.gameObject.SetActive(false);
            _slotButton.SetActive(false);
        } catch (Exception) { };
        resourceShower.SetActive(false);
        select.transform.position = new Vector3(123564, 125354);
    }

    public void SelectClear() {
        _select = null;
    }

    IEnumerator Refresh() {
        while (true) {
            yield return new WaitForSeconds(0.5f);
            OnTouch(_select);
        }
    }

    public void OnOpenClick() {
        FindObjectOfType<InventoryGuiManager>().Open(_inventory);
    }
}
