using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuildGuiManager :Manager {
    public GameObject content;
    public GameObject buildingButtonPrefab;
    public GameObject buildHereObject;
    public Scrollbar scrollbarObject;

    private BuildingButton[] _buttons;
    private BuildingManager _buildingManager;
    private TextManager _textManager;
    private SpriteManager _spriteManager;
    private AlertManager _alertManager;
    private AreaManager _areaManager;
    private RiceCakeManager _riceCakeManager;
    private ShowerManager _showerManager;
    private AudioManager _audioManager;
    private ValueManager _valueManager;
    private BuildingInfo _selectBuildingInfo;
    private Transform _lookAtMe;
    private GameObject _selectBackground;

    void Awake() {
        ManagerManager.SetManagers(this);
        _lookAtMe = FindObjectOfType<LookAtMe>().transform;
        _selectBackground = content.transform.Find("Select").gameObject;
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
        _selectBackground.transform.SetSiblingIndex(99);
        _selectBuildingInfo = _buildingManager.GetBuildingInfo("fan");
    }

    public void OnBuildingButtonClick(string name) {
        foreach (var bb in _buttons) {
            if (bb.buildingName.Equals(name)) {
                _selectBackground.transform.position = bb.transform.position;
            }
        }
        _selectBuildingInfo = _buildingManager.GetBuildingInfo(name);
        _showerManager.OnTouch(_selectBuildingInfo);
    }

    public void OnFoldButtonClick() {
        StartCoroutine(ProcShower());
    }

    IEnumerator ProcShower() {
        yield return new WaitForSeconds(0.3f);
        if (buildHereObject.activeInHierarchy) {
            _showerManager.OnTouch(_selectBuildingInfo);
            var infos = _buildingManager.buildingInfos;
            var i = 0;
            for (i = 0; i < infos.Length; i++) {
                if (infos[i].Equals(_selectBuildingInfo))
                    break;
            }
            scrollbarObject.value = 1f - i / (float)infos.Length;
        } else {
            _showerManager.OffAll(true);
        }
    }

    public void OnBuildButtonClick() {
        if (_selectBuildingInfo == null)
            return;
        if (_valueManager.GetValue("gustn").Value < _selectBuildingInfo.price) {
            _alertManager.AddAlert("notEnoughItem", Color.red);
            return;
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

        _valueManager.GetValue("gustn").defaultValue -= _selectBuildingInfo.price;
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