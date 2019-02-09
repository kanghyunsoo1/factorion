using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class InventoryManager :MonoBehaviour {
    private InventoryHolder _baseIH;
    void Awake() {
        _baseIH = FindObjectOfType<Base>().GetComponent<InventoryHolder>();
    }
    private void Start() {
        AddItem("iron", 50);
        AddItem("copper", 50);
    }
    public InventorySlot[] GetSlots() {
        return _baseIH.GetSlots();
    }

    public int GetItemCount(string name) {
        return _baseIH.GetItemCount(name);
    }

    public void AddItem(string name, int count) {
        _baseIH.AddItem(name, count);
    }

    public void RemoveItem(string name, int count) {
        _baseIH.RemoveItem(name, count);
    }
}
