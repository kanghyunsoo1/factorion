using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowerManager :MonoBehaviour {
    public GameObject infoShower;
    public GameObject resourceShower;
    public GameObject select;
    public GameObject inventoryShower;
    public GameObject robotShower;
    public GameObject requestInventoryShower;

    private TextManager _textManager;
    private SpriteManager _spriteManager;
    private Text _infoNameText;
    private Text _infoDescriptionText;
    private Text _resourceNameText;
    private Text _resourceAmountText;
    private Text _robotText;
    private Image _resourceImage;
    private RobotContainer _robotContainer;
    private Resource _resource;

    void Awake() {
        _textManager = GetComponent<TextManager>();
        _spriteManager = GetComponent<SpriteManager>();
        _infoNameText = infoShower.transform.Find("NameText").GetComponent<Text>();
        _infoDescriptionText = infoShower.transform.Find("DescriptionText").GetComponent<Text>();
        _resourceNameText = resourceShower.transform.Find("NameText").GetComponent<Text>();
        _resourceAmountText = resourceShower.transform.Find("AmountText").GetComponent<Text>();
        _resourceImage = resourceShower.transform.Find("Image").GetComponent<Image>();
        _robotText = robotShower.transform.Find("AmountText").GetComponent<Text>();
    }

    public void OnTouch(GameObject go) {
        if (go == null)
            return;
        OffAll();
        StartCoroutine(Refresh());
        var inventory = go.GetComponent<Inventory>();
        var requestInventory = go.GetComponent<RequestInventory>();

        _resource = go.GetComponent<Resource>();
        _robotContainer = go.GetComponent<RobotContainer>();
        RefreshResource();
        RefreshRobot();
        if (inventory != null) {
            inventoryShower.SetActive(true);
            inventoryShower.GetComponent<InventoryShower>().SetInventory(inventory, go.name, false);
        }
        if (requestInventory != null) {
            requestInventoryShower.SetActive(true);
            requestInventoryShower.GetComponent<InventoryShower>().SetInventory(requestInventory, go.name, true);
        }
        infoShower.SetActive(true);
        var objName = go.name.Replace("(Clone)", "").ToLower();
        var tName = _textManager.GetText("name", objName);
        var tDes = _textManager.GetText("des", objName);
        _infoNameText.text = tName;
        _infoDescriptionText.text = tDes;
        select.transform.position = go.transform.position;
    }

    void RefreshRobot() {
        if (_robotContainer != null) {
            robotShower.SetActive(true);
            _robotText.text = _robotContainer.count + "";
        }
    }

    void RefreshResource() {
        if (_resource != null) {
            resourceShower.SetActive(true);
            _resourceNameText.text = _textManager.GetText("item", _resource.name);
            _resourceAmountText.text = _resource.amount + "";
            _resourceImage.sprite = _spriteManager.GetSprite("item", _resource.name);
        }
    }

    public void OffAll() {
        infoShower.SetActive(false);
        inventoryShower.GetComponent<InventoryShower>().Clear();
        inventoryShower.SetActive(false);
        resourceShower.SetActive(false);
        robotShower.SetActive(false);
        requestInventoryShower.GetComponent<InventoryShower>().Clear();
        requestInventoryShower.SetActive(false);
        select.transform.position = new Vector3(123564, 125354);
        StopAllCoroutines();
    }

    IEnumerator Refresh() {
        while (true) {
            yield return new WaitForSeconds(0.3f);
            RefreshRobot();
            RefreshResource();
        }
    }
}