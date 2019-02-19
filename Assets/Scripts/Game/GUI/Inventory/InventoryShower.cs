using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryShower :MonoBehaviour {

    public GameObject inventorySlot;

    private ShowerInventorySlot[] _slots;
    private SpriteManager _sm;
    private ItemContainer _inventory;
    private string _owner;
    private bool _isRequset;

    void Awake() {
        _sm = FindObjectOfType<SpriteManager>();
        _slots = new ShowerInventorySlot[] {
            Instantiate(inventorySlot).GetComponent<ShowerInventorySlot>()
            ,Instantiate(inventorySlot).GetComponent<ShowerInventorySlot>()
            ,Instantiate(inventorySlot).GetComponent<ShowerInventorySlot>()
            ,Instantiate(inventorySlot).GetComponent<ShowerInventorySlot>()
        };
        for (int i = 0; i < 4; i++) {
            _slots[i].transform.SetParent(transform);
            _slots[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(i * 50, 0);
            _slots[i].gameObject.SetActive(false);
        }
    }
    
    public void SetInventory(ItemContainer inv, string owner, bool isRequest) {
        _inventory = inv;
        _owner = owner;
        _isRequset = isRequest;
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

            var stacks = _inventory.GetStacks();
            int max = stacks.Length;
            if (max > 4) {
                max = 4;
            }
            for (int i = 0; i < max; i++) {
                _slots[i].gameObject.SetActive(true);
                _slots[i].SetItem(_sm.GetSprite("item", stacks[i].name), stacks[i].count);
            }
        }
    }

    public void OnOpenClick() {
        FindObjectOfType<InventoryGuiManager>().Open(_inventory, _owner, _isRequset);
    }
}
