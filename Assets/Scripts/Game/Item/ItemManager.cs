using System;
using UnityEngine;

public class ItemManager :MonoBehaviour {
    public ItemInfo[] itemInfos;

    private void Awake() {
        itemInfos = new ItemInfo[]{
             Init("raw-coal")
            ,Init("raw-iron")
            ,Init("raw-copper")
            ,Init("rice")
            ,Init("water")
            ,Init("raw-dudxo")
            ,Init("rice-flour", "rice:1", 10)
            ,Init("clean-water","water:1",10)
            ,Init("iron","raw-iron:1",10)
            ,Init("copper","raw-copper:1",10)
            ,Init("coal","raw-coal:1, iron:1, copper:1",100)
            ,Init("dudxo-flour","raw-dudxo:1",100)
        };
    }

    private ItemInfo Init(string name, string needs = null, int energy = 0) {
        if (needs == null)
            return new ItemInfo(name);
        return new ItemInfo(name, needs, energy);
    }

    public ItemInfo GetItemInfo(string name) {
        return Array.Find(itemInfos, x => x.name.Equals(name));
    }
}