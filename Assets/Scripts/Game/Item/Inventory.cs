using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory :KhsComponent {
    
    public List<ItemStack> _stackList;
    void Awake() {
        _stackList = new List<ItemStack>();
    }
    
    public ItemStack[] GetStacks() {
        return _stackList.ToArray();
    }

    public int GetItemCount(string name) {

        var slot = _stackList.Find(x => x.name.Equals(name));
        if (slot == null)
            return 0;
        return slot.count;

    }

    public void AddItem(string name, int count) {
        for (int i = 0; i < _stackList.Count; i++) {
            if (_stackList[i].name.Equals(name)) {
                _stackList[i].count += count;
                return;
            }
        }
        _stackList.Add(new ItemStack() { name = name, count = count });
    }

    public void RemoveItem(string name, int count) {
        var slot = _stackList.Find(x => x.name.Equals(name));
        if (slot == null)
            return;
        if (slot.count >= count) {
            slot.count -= count;
        } else
            _stackList.Remove(slot);
    }
}
