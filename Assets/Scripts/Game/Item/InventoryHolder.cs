using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder :KhsComponent {
    
    public List<InventorySlot> _slotList;
    void Awake() {
        _slotList = new List<InventorySlot>();
    }
    
    public InventorySlot[] GetSlots() {
        return _slotList.ToArray();
    }

    public int GetItemCount(string name) {

        var slot = _slotList.Find(x => x.name.Equals(name));
        if (slot == null)
            return 0;
        return slot.count;

    }

    public void AddItem(string name, int count) {
        for (int i = 0; i < _slotList.Count; i++) {
            if (_slotList[i].name.Equals(name)) {
                _slotList[i].count += count;
                return;
            }
        }
        _slotList.Add(new InventorySlot() { name = name, count = count });
    }

    public void RemoveItem(string name, int count) {
        var slot = _slotList.Find(x => x.name.Equals(name));
        if (slot == null)
            return;
        if (slot.count >= count) {
            slot.count -= count;
        } else
            _slotList.Remove(slot);
    }
}
