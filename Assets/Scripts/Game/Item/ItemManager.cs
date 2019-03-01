using System;
using UnityEngine;

public class ItemManager :MonoBehaviour {
    public ItemInfo[] itemInfos;

    private void Awake() {
        itemInfos = new ItemInfo[]{
             Init("raw-coal")
            ,Init("raw-iron")
            ,Init("raw-copper")
            ,Init("raw-rice")
            ,Init("dirty-water")
            ,Init("raw-dudxo")
            ,Init("rice", "raw-rice:1,clean-water:1", 10,3f)
            ,Init("clean-water","dirty-water:1",10,3f)
            ,Init("iron-bar","raw-iron:1",10,3f)
            ,Init("copper-bar","raw-copper:1",10,3f)
            ,Init("processed-coal","raw-coal:1, iron:10, copper:10",100,20f)
            ,Init("rice-flour","rice:1,clean-water:1",10,1f)
        };
    }

    private ItemInfo Init(string name, string needs = null, int energy = 0, float time = 0f) {
        if (needs == null)
            return new ItemInfo(name);
        return new ItemInfo(name, needs, energy, time);
    }

    public ItemInfo GetItemInfo(string name) {
        return Array.Find(itemInfos, x => x.name.Equals(name));
    }
}