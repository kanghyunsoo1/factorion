using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildGuiManager :MonoBehaviour {
    public SpriteRenderer center;
    public GameObject content;
    public GameObject buildingButtonPrefab;

    private BuildingButton[] _buttons;
    private BuildingManager _bm;
    private TextManager _tm;
    private SpriteManager _sm;
    private AlertManager _alm;
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
        _requiredHolders = FindObjectsOfType<BuildingRequiredItemsHolder>();
        _build = GameObject.Find("Canvas").transform.Find("Build").Find("Build").gameObject;
    }

    private void Start() {

        _buttons = new BuildingButton[_bm.buildingInfos.Length];
        for (int i = 0; i < _bm.buildingInfos.Length; i++) {
            var go = (GameObject)Instantiate(buildingButtonPrefab);
            go.transform.SetParent(content.transform);
            _buttons[i] = go.GetComponent<BuildingButton>();
            _buttons[i].buildingName = _bm.buildingInfos[i].name;
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i * -100);
        }
        StartCoroutine(RefreshLoop());
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
        var baseInventory = FindObjectOfType<Base>().GetComponent<Inventory>();
        for (int i = 0; i < _select.needStacks.Length; i++) {
            _requiredHolders[i].gameObject.SetActive(true);
            var item = _select.needStacks[i].name;
            _requiredHolders[i].SetItemInfo(_tm.GetText("item", item)
                , _sm.GetSprite("item", item)
                , _select.needStacks[i].count
                , baseInventory.GetItemCount(item));
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
        if (_select == null)
            return;
        var baseInventory = FindObjectOfType<Base>().GetComponent<Inventory>();
        foreach (var invs in _select.needStacks) {
            if (baseInventory.GetItemCount(invs.name) < invs.count) {
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

        foreach (var invs in _select.needStacks) {
            baseInventory.PullItem(invs.name, invs.count);
        }
        var go = _km.Instantiate(_select.name);
        go.transform.position = center.transform.position;

        if (_select.name.Equals("miner")) {
            var res = user.GetComponent<Resource>();
            go.GetComponent<Resource>().name = res.name;
            go.GetComponent<Resource>().amount = res.amount;
            Destroy(user.gameObject);
        }
        _alm.AddAlert("build", Color.black);
    }
}