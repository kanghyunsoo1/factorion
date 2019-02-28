﻿using System;
[Serializable]
public class ItemInfo {
    public string name;
    public ItemBundle[] requireBundles;
    public int needEnergy;

    public ItemInfo(string name) {
        this.name = name;
    }

    public ItemInfo(string name, string items, int needEnergy) {
        this.name = name;
        this.requireBundles = ItemBundle.GetBundles(items);
        this.needEnergy = needEnergy;
    }
}