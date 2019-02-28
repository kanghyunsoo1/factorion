using System.Collections.Generic;

public class ItemContainer :RiceCakeComponent {
    public List<ItemBundle> _bundleList;

    void Awake() {
        _bundleList = new List<ItemBundle>();
    }

    public ItemBundle[] GetBundles() {
        return _bundleList.ToArray();
    }

    public int GetItemCount(string name) {
        var stack = _bundleList.Find(x => x.name.Equals(name));
        if (stack == null)
            return 0;
        return stack.count;

    }

    public void AddItem(string name, int count) {
        for (int i = 0; i < _bundleList.Count; i++) {
            if (_bundleList[i].name.Equals(name)) {
                _bundleList[i].count += count;
                return;
            }
        }
        _bundleList.Add(new ItemBundle(name, count));
    }
    public int PullItem(string name, int count) {

        var stack = _bundleList.Find(x => x.name.Equals(name));
        if (stack == null)
            return 0;

        if (stack.count >= count) {
            stack.count -= count;
            if (stack.count == 0)
                _bundleList.Remove(stack);
            return count;
        } else {
            var t = stack.count;
            _bundleList.Remove(stack);
            return t;
        }
    }
}