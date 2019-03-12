using System.Collections;
using UnityEngine;

public class BuildGuiManager :Manager {
    public GameObject content;
    public GameObject buildingButtonPrefab;
    public GameObject buildHereObject;

    private BuildingButton[] _buttons;
    private BuildingManager _buildingManager;
    private TextManager _textManager;
    private SpriteManager _spriteManager;
    private AlertManager _alertManager;
    private AreaManager _areaManager;
    private RiceCakeManager _riceCakeManager;
    private BuildingInfo _selectBuildingInfo;
    private BuildingRequiredItemsHolder[] _requiredHolders;
    private Transform _lookAtMe;
    private GameObject _select;

    void Awake() {
        _alertManager = ManagerManager.GetManager<AlertManager>();
        _textManager = ManagerManager.GetManager<TextManager>();
        _spriteManager = ManagerManager.GetManager<SpriteManager>();
        _areaManager = ManagerManager.GetManager<AreaManager>();
        _riceCakeManager = ManagerManager.GetManager<RiceCakeManager>();
        _buildingManager = ManagerManager.GetManager<BuildingManager>();
        _requiredHolders = FindObjectsOfType<BuildingRequiredItemsHolder>();
        _lookAtMe = FindObjectOfType<LookAtMe>().transform;
        _select = content.transform.Find("Select").gameObject;
        buildHereObject.SetActive(false);
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

        OnBuildingButtonClick("fan");
    }




    public void OnBuildingButtonClick(string name) {
        _select.transform.SetSiblingIndex(99);
        foreach (var bb in _buttons) {
            if (bb.buildingName.Equals(name)) {
                _select.transform.position = bb.transform.position;
            }
        }
        _selectBuildingInfo = _buildingManager.GetBuildingInfo(name);
        FindObjectOfType<BuildingNameHolder>().SetText(_textManager.GetText("name", name));
        FindObjectOfType<BuildingDesHolder>().SetText(_textManager.GetText("des", name));
        foreach (var a in _requiredHolders) {
            a.gameObject.SetActive(false);
        }
        RefreshRequires();
    }

    void RefreshRequires() {
        var baseInventory = FindObjectOfType<Warehouse>().GetComponent<Inventory>();
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
        var baseInventory = FindObjectOfType<Warehouse>().GetComponent<Inventory>();
        foreach (var bundle in _selectBuildingInfo.requireBundles) {
            if (baseInventory.GetItemCount(bundle.name) < bundle.count) {
                _alertManager.AddAlert("notEnoughItem", Color.red);
                return;
            }
        }
        var user = _areaManager.GetUser(buildHereObject.transform.position);
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
        go.transform.position = buildHereObject.transform.position;
        _alertManager.AddAlert("build", Color.black);
    }

    void Update() {
        if (!buildHereObject.activeInHierarchy && content.transform.localScale.x == 1f) {
            buildHereObject.SetActive(true);
        } else if (content.transform.localScale.x == 0f) {

            buildHereObject.SetActive(false);
        }
        buildHereObject.transform.position = new Vector3(Mathf.Round(_lookAtMe.position.x), 0f, Mathf.Round(_lookAtMe.position.z));

    }
}