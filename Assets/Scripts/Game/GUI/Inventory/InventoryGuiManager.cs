using UnityEngine;
using UnityEngine.UI;
public class InventoryGuiManager :MonoBehaviour {
    public GameObject inventoryObject;
    public GameObject slotPrefab;

    private readonly int _x = 6, _y = 15, _size = 80;
    private InventorySlot[] _slots;
    private Text _nameText, _countText;
    private TextManager _textManager;
    private ItemBundle _selectBundle;
    private Inventory _selectInventory;
    private int _refreshCount;
    private bool _isOpen = true;
    private bool _isGoingBack = true;
    private Vector3 _savedCameraPosition;
    private Camera _camera;

    private void Awake() {
        _camera = Camera.main;
        _textManager = GetComponent<TextManager>();
        _nameText = inventoryObject.transform.Find("Name").GetComponent<Text>();
        _countText = inventoryObject.transform.Find("Count").GetComponent<Text>();
        _slots = new InventorySlot[_x * _y];
        int i = 0;
        for (int y = 0; y < _y; y++)
            for (int x = 0; x < _x; x++) {
                _slots[i] = Instantiate(slotPrefab).GetComponent<InventorySlot>();
                _slots[i].transform.SetParent(inventoryObject.transform);
                _slots[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(x * _size, -y * _size);
                _slots[i].gameObject.SetActive(false);
                i++;
            }
        Close();
    }

    public void SwitchBase() {
        if (_isOpen) {
            Close();
        } else {
            OpenBase();
        }
    }

    public void Switch(Inventory inventory, string owner) {
        if (_isOpen) {
            Close();
        } else {
            Open(inventory, owner);
        }
    }

    public void OpenBase() {
        Open(FindObjectOfType<Warehouse>().GetComponent<Inventory>(), "base");
    }

    public void Open(Inventory inventory, string owner) {
        Close();
        _savedCameraPosition = _camera.transform.position;
        _isGoingBack = false;
        _isOpen = true;
        _selectInventory = inventory;
        inventoryObject.SetActive(true);
        SetInventoryAndSlots();
        if (_slots[0].isActiveAndEnabled) {
            _slots[0].GetComponent<Button>().onClick.Invoke();
        }
        var inv = _textManager.GetText("gui", "inventory");
        inventoryObject.transform.Find("Text").GetComponent<Text>().text = string.Format("{0} -> {1}", _textManager.GetText("name", owner), inv);
    }

    private void SetInventoryAndSlots() {
        var items = _selectInventory.GetBundles();
        inventoryObject.GetComponent<RectTransform>().sizeDelta = new Vector2(_size * _x, items.Length / _x * _size + _size + 50);
        for (int i = 0; i < items.Length; i++) {
            _slots[i].SetBundle(items[i]);
            _slots[i].gameObject.SetActive(true);
        }
    }

    public void Close() {
        if (!_isOpen)
            return;
        _nameText.text = "";
        _countText.text = "";
        foreach (var i in _slots)
            i.gameObject.SetActive(false);
        inventoryObject.SetActive(false);
        _selectInventory = null;
        _selectBundle = null;
        _isOpen = false;
        _isGoingBack = true;
    }

    public void OnSlotClick(ItemBundle bundle) {
        _selectBundle = bundle;
    }

    public void FixedUpdate() {
        if (_selectInventory == null)
            return;
        if (++_refreshCount > 100) {
            SetInventoryAndSlots();
            _refreshCount = 0;
        }
        if (_selectBundle == null)
            return;
        _nameText.text = _textManager.GetText("item", _selectBundle.name);
        _countText.text = _selectBundle.count + "";
    }

    public void Update() {
        var target = _camera.transform.position;
        if (_isOpen) {
            target = _selectInventory.transform.position;
        } else {
            if (_isGoingBack) {
                target = _savedCameraPosition;
            }
        }
        target.z = -10;
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, target, 0.2f);
        if (Vector2.Distance(_camera.transform.position, _savedCameraPosition) < 0.1f)
            _isGoingBack = false;
    }
}