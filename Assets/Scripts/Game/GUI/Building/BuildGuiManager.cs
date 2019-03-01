using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildGuiManager :MonoBehaviour {
    public SpriteRenderer center;
    public GameObject content;
    public GameObject buildingButtonPrefab;

    private BuildingButton[] _buttons;
    private BuildingManager _buildingManager;
    private TextManager _textManager;
    private SpriteManager _spriteManager;
    private AlertManager _alertManager;
    private AreaManager _areaManager;
    private RiceCakeManager _riceCakeManager;
    private BuildingInfo _selectBuildingInfo;
    private BuildingRequiredItemsHolder[] _requiredHolders;
    private GameObject _build;

    void Awake() {
        _alertManager = GetComponent<AlertManager>();
        _textManager = GetComponent<TextManager>();
        _spriteManager = GetComponent<SpriteManager>();
        _areaManager = GetComponent<AreaManager>();
        _riceCakeManager = GetComponent<RiceCakeManager>();
        _buildingManager = GetComponent<BuildingManager>();
        _requiredHolders = FindObjectsOfType<BuildingRequiredItemsHolder>();
        _build = GameObject.Find("Canvas").transform.Find("Build").Find("Build").gameObject;
    }

    private void Start() {

        _buttons = new BuildingButton[_buildingManager.buildingInfos.Length];
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, _buildingManager.buildingInfos.Length * 100);
        for (int i = 0; i < _buildingManager.buildingInfos.Length; i++) {
            var go = (GameObject)Instantiate(buildingButtonPrefab);
            go.transform.SetParent(content.transform);
            _buttons[i] = go.GetComponent<BuildingButton>();
            _buttons[i].buildingName = _buildingManager.buildingInfos[i].name;
            go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, i * -100);
        }
        StartCoroutine(RefreshLoop());

        OnBuildingButtonClick("miner");
    }

    private void FixedUpdate() {
        if (_build.transform.localScale.y == 0f) {
            center.color = new Color(1f, 1f, 1f, 0f);
        } else {
            if (_areaManager.GetUser(center.transform.position) == null) {
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
        _selectBuildingInfo = _buildingManager.GetBuildingInfo(name);
        FindObjectOfType<BuildingNameHolder>().SetText(_textManager.GetText("name", name));
        FindObjectOfType<BuildingDesHolder>().SetText(_textManager.GetText("des", name));
        center.sprite = _spriteManager.GetSprite("block", name);
        foreach (var a in _requiredHolders) {
            a.gameObject.SetActive(false);
        }
        RefreshRequires();
    }

    void RefreshRequires() {
        var baseInventory = FindObjectOfType<Base>().GetComponent<Inventory>();
        for (int i = 0; i < _selectBuildingInfo.requireBundles.Length; i++) {
            _requiredHolders[i].gameObject.SetActive(true);
            var item = _selectBuildingInfo.requireBundles[i].name;
            _requiredHolders[i].SetItemInfo(_textManager.GetText("item", item)
                , _spriteManager.GetSprite("item", item)
                , _selectBuildingInfo.requireBundles[i].count
                , baseInventory.GetItemCount(item));
        }
    }

    IEnumerator RefreshLoop() {
        while (true) {
            yield return new WaitForSeconds(1f);
            if (_selectBuildingInfo != null)
                RefreshRequires();
        }
    }

    public void OnBuildButtonClick() {
        if (_selectBuildingInfo == null)
            return;
        var baseInventory = FindObjectOfType<Base>().GetComponent<Inventory>();
        foreach (var bundle in _selectBuildingInfo.requireBundles) {
            if (baseInventory.GetItemCount(bundle.name) < bundle.count) {
                _alertManager.AddAlert("notEnoughItem", Color.red);
                return;
            }
        }
        var user = _areaManager.GetUser(center.transform.position);
        if (_selectBuildingInfo.name.Equals("miner")) {
            if (user == null) {
                _alertManager.AddAlert("minerShould", Color.red);
                return;
            }
        } else if (user != null) {
            _alertManager.AddAlert("alreadyExists", Color.red);
            return;
        }

        foreach (var bundle in _selectBuildingInfo.requireBundles) {
            baseInventory.PullItem(bundle.name, bundle.count);
        }
        var go = _riceCakeManager.Instantiate(_selectBuildingInfo.name);
        go.transform.position = center.transform.position;
        _alertManager.AddAlert("build", Color.black);
    }
}