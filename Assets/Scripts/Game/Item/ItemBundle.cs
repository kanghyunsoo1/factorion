using System;

[Serializable]
public class ItemBundle {
    public string name;
    public int count;

    public ItemBundle(string txt) {// "name:5"
        var s = txt.Split(':');
        name = s[0].Trim();
        count = int.Parse(s[1]);
    }

    public ItemBundle(string name, int count) {
        this.name = name;
        this.count = count;
    }

    public static ItemBundle[] GetBundles(string txt) {
        var comma = txt.Split(',');
        int count = comma.Length;
        var bundles = new ItemBundle[count];
        for (int i = 0; i < count; i++) {
            bundles[i] = new ItemBundle(comma[i]);
        }
        return bundles;
    }
}