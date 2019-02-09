using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildGuiManager :MonoBehaviour {
    public SpriteRenderer center;

    private BuildingButton[] _buttons;
    private BuildingManager _bm;
    private TextManager _tm;
    private SpriteManager _sm;
    private AlertManager _alm;
    private InventoryManager _inm;
    private AreaManager _am;
    private KhsManager _km;
    private BuildingInfo _select;
    private BuildingRequiredItemsHolder[] _requiredHolders;
    private GameObject _build;
    void Awake() {
        _alm = GetComponent<AlertManager>();
        _tm = GetComponent<TextManager>();
        _sm = GetComponent<SpriteManager>();
        _am = GetComponent<AreaManager>();
        _km = GetComponent<KhsManager>();
        _bm = GetComponent<BuildingManager>();
        _inm = GetComponent<InventoryManager>();
        _buttons = FindObjectsOfType<BuildingButton>();
        _requiredHolders = FindObjectsOfType<BuildingRequiredItemsHolder>();
        _build = GameObject.Find("Canvas").transform.Find("Build").Find("Build").gameObject;
    }

    private void FixedUpdate() {
        if (_build.transform.localScale.y == 0f) {
            center.color = new Color(1f, 1f, 1f, 0f);
        } else {
            if (_am.GetUser(center.transform.position) == null) {
                center.color = new Color(1f, 1f, 1f, 0.7f);
            } else {
                center.color = new Color(1f, 0f, 0f, 0.7f);
            }
        }
    }

    private void Start() {
        OnBuildingButtonClick("miner");
        StartCoroutine(RefreshLoop());
    }

    public void OnBuildingButtonClick(string name) {
        foreach (var bb in _buttons) {
            if (bb.buildingName.Equals(name))
                bb.GetComponent<Image>().color = new Color(1f, 0.8f, 0.8f, 1f);
            else
                bb.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        _select = _bm.GetBuildingInfo(name);
        FindObjectOfType<BuildingNameHolder>().SetText(_tm.GetText("name", name));
        FindObjectOfType<BuildingDesHolder>().SetText(_tm.GetText("des", name));
        center.sprite = _sm.GetSprite("block", name);
        foreach (var a in _requiredHolders) {
            a.gameObject.SetActive(false);
        }
        RefreshRequires();
    }

    void RefreshRequires() {
        for (int i = 0; i < _select.requiredItems.Length; i++) {
            _requiredHolders[i].gameObject.SetActive(true);
            var item = _select.requiredItems[i];
            _requiredHolders[i].SetItemInfo(_tm.GetText(item)
                , _sm.GetSprite("item", item)
                , _select.requiredCounts[i]
                , _inm.GetItemCount(item));
        }
    }

    IEnumerator RefreshLoop() {
        while (true) {
            yield return new WaitForSeconds(1f);
            if (_select != null)
                RefreshRequires();
        }
    }

    public void OnBuildButtonClick() {
        for (int i = 0; i < _select.requiredCounts.Length; i++) {
            if (_inm.GetItemCount(_select.requiredItems[i]) < _select.requiredCounts[i]) {
                _alm.AddAlert("notEnoughItem", Color.red);
                return;
            }
        }
        var user = _am.GetUser(center.transform.position);
        if (_select.name.Equals("miner")) {
            if (user == null) {
                _alm.AddAlert("minerShould", Color.red);
                return;
            }
            if (user.GetComponent<Resource>() == null || user.GetComponent<Miner>() != null) {
                _alm.AddAlert("alreadyExists", Color.red);
                return;
            }
        } else if (user != null) {
            _alm.AddAlert("alreadyExists", Color.red);
            return;
        }

        for (int i = 0; i < _select.requiredCounts.Length; i++) {
            _inm.RemoveItem(_select.requiredItems[i], _select.requiredCounts[i]);
        }
        var go = _km.Instantiate(_select.name);
        go.transform.position = center.transform.position;

        if (_select.name.Equals("miner")) {
            var res = user.GetComponent<Resource>();
            go.GetComponent<Resource>().name = res.name;
            go.GetComponent<Resource>().amount = res.amount;
            Destroy(user.gameObject);
        }
    }

}