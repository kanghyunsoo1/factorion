using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class InventoryManager :MonoBehaviour {
    private List<InventorySlot> _slotList;

    void Awake() {
        _slotList = new List<InventorySlot>();
    }

    public int GetItemCount(string name) {

        var slot = _slotList.Find(x => x.name.Equals(name));
        if (slot == null)
            return 0;
        return slot.count;

    }

    public void PushItem(string name, int count) {
        for (int i = 0; i < _slotList.Count; i++) {
            if (_slotList[i].name.Equals(name))
                _slotList[i].count += count;
            return;
        }
        _slotList.Add(new InventorySlot() { name = name, count = count });
    }

    public void PopItem(string name, int count) {
        var slot = _slotList.Find(x => x.name.Equals(name));
        if (slot == null)
            return;
        if (slot.count >= count) {
            slot.count -= count;
        } else
            _slotList.Remove(slot);
    }

    public void Save(string name) {
        var path = Application.persistentDataPath + "/" + name;
        Directory.CreateDirectory(path);
        var sb = new StringBuilder();
        foreach (var slot in _slotList) {
            sb.Append(slot.name);
            sb.Append("#");
            sb.Append(slot.count);
            sb.AppendLine();
        }
        File.WriteAllText(path + "/inventory.khs", sb.ToString());
    }

    public void Load(string name) {
        var path = Application.persistentDataPath + "/" + name;
        if (!File.Exists(path + "/inventory.khs"))
            return;
        var lines = File.ReadAllLines(path + "/inventory.khs");
        _slotList.Clear();
        foreach (string line in lines) {
            if (line.Trim().Equals(""))
                return;
            var slot = new InventorySlot();
            var arr = line.Split('#');
            slot.name = arr[0];
            slot.count = int.Parse(arr[1]);
            _slotList.Add(slot);
        }
    }
    public void Delete(string name) {
        var path = Application.persistentDataPath + "/" + name;
        File.Delete(path + "/inventory.khs");
    }
}
