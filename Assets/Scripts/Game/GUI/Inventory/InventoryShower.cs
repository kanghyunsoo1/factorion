using System.Collections;
using UnityEngine;

public class InventoryShower :MonoBehaviour {
    public GameObject inventorySlot;

    private ShowerInventorySlot[] _slots;
    private SpriteManager _spriteManager;
    private Inventory _inventory;
    private string _owner;

    void Awake() {
        _spriteManager = ManagerManager.GetManager<SpriteManager>();
        _slots = new ShowerInventorySlot[] {
            Instantiate(inventorySlot).GetComponent<ShowerInventorySlot>()
            ,Instantiate(inventorySlot).GetComponent<ShowerInventorySlot>()
            ,Instantiate(inventorySlot).GetComponent<ShowerInventorySlot>()
            ,Instantiate(inventorySlot).GetComponent<ShowerInventorySlot>()
        };
        for (int i = 0; i < 4; i++) {
            _slots[i].transform.SetParent(transform);
            _slots[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 70, 0);
            _slots[i].gameObject.SetActive(false);
        }
    }

    public void SetInventory(Inventory inv, string owner) {
        _inventory = inv;
        _owner = owner;
        StartCoroutine(Loop());
    }

    public void Clear() {
        foreach (var i in _slots) {
            i.gameObject.SetActive(false);
        }
        _inventory = null;
    }

    IEnumerator Loop() {
        while (true) {
            yield return new WaitForSeconds(0.3f);
            if (_inventory == null)
                continue;

            var items = _inventory.GetBundles();
            int max = items.Length;
            if (max > 4) {
                max = 4;
            }
            for (int i = 0; i < max; i++) {
                _slots[i].gameObject.SetActive(true);
                _slots[i].SetItem(_spriteManager.GetSprite("item", items[i].name), items[i].count);
            }
        }
    }

    public void OnOpenClick() {
        ManagerManager.GetManager<InventoryGuiManager>().Switch(_inventory, _owner);
    }
}