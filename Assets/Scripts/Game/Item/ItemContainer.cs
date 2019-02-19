using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : KhsComponent {

    public List<ItemStack> _stackList;
    
    void Awake() {
        _stackList = new List<ItemStack>();
    }

    public ItemStack[] GetStacks() {
        return _stackList.ToArray();
    }

    public int GetItemCount(string name) {

        var stack = _stackList.Find(x => x.name.Equals(name));
        if (stack == null)
            return 0;
        return stack.count;

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
    public int PullItem(string name, int count) {

        var stack = _stackList.Find(x => x.name.Equals(name));
        if (stack == null)
            return 0;

        if (stack.count >= count) {
            stack.count -= count;
            if (stack.count == 0)
                _stackList.Remove(stack);
            return count;
        } else {
            var t = stack.count;
            _stackList.Remove(stack);
            return t;
        }
    }
}
