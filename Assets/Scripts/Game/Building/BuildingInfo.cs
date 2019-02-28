using System;

[Serializable]
public class BuildingInfo {
    public string name;
    public ItemBundle[] requireBundles;

    public BuildingInfo(string name, string txt) {
        this.name = name;
        this.requireBundles = ItemBundle.GetBundles(txt);
    }
}