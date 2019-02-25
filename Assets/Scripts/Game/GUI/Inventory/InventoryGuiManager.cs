﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryGuiManager :MonoBehaviour {
    public GameObject inventoryObject;
    public GameObject slotPrefab;

    private readonly int _x = 6, _y = 15, _size = 80;
    private InventorySlot[] _slots;
    private Text _name, _count;
    private TextManager _tm;
    private ItemStack _selectStack;
    private ItemContainer _selectInventory;
    private int _refreshCount;
    private bool _isOpen = true;

    private void Awake() {
        _tm = GetComponent<TextManager>();
        _name = inventoryObject.transform.Find("Name").GetComponent<Text>();
        _count = inventoryObject.transform.Find("Count").GetComponent<Text>();
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
        Switch();
    }

    public void Switch() {
        if (_isOpen) {
            Close();
        } else {
            OpenBase();
        }
    }

    public void OpenBase() {
        Open(FindObjectOfType<Base>().GetComponent<ItemContainer>(), "base", false);
    }

    public void Open(ItemContainer inventory, string owner, bool isRequest) {
        Close();
        _selectInventory = inventory;
        inventoryObject.SetActive(true);
        SetInventoryAndSlots();
        if (_slots[0].isActiveAndEnabled) {
            _slots[0].GetComponent<Button>().onClick.Invoke();
        }
        _isOpen = true;
        var inv = _tm.GetText("inventory");
        if (isRequest)
            inv = _tm.GetText("request-inventory");
        inventoryObject.transform.Find("Text").GetComponent<Text>().text = string.Format("{0} -> {1}", _tm.GetText("name", owner), inv);
    }

    private void SetInventoryAndSlots() {
        var stacks = _selectInventory.GetStacks();
        inventoryObject.GetComponent<RectTransform>().sizeDelta = new Vector2(_size * _x, stacks.Length / _x * _size + _size + 50);
        for (int i = 0; i < stacks.Length; i++) {
            _slots[i].SetStack(stacks[i]);
            _slots[i].gameObject.SetActive(true);
        }
    }

    public void Close() {
        _name.text = "";
        _count.text = "";
        foreach (var i in _slots)
            i.gameObject.SetActive(false);
        inventoryObject.SetActive(false);
        _selectInventory = null;
        _selectStack = null;
        _isOpen = false;
    }

    public void OnSlotClick(ItemStack stack) {
        _selectStack = stack;
    }

    public void FixedUpdate() {
        if (_selectInventory == null)
            return;
        if (++_refreshCount > 100) {
            SetInventoryAndSlots();
            _refreshCount = 0;
        }
        if (_selectStack == null)
            return;

        _name.text = _tm.GetText("item", _selectStack.name);
        _count.text = _selectStack.count + "";
    }
}