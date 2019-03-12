using System;
using UnityEngine;

public class ItemManager :Manager {
    public ItemInfo[] itemInfos;

    private void Awake() {
        itemInfos = new ItemInfo[]{
             Init("raw-coal")
            ,Init("processed-coal","raw-coal:1, iron:10, copper:10",100,20f)

            ,Init("raw-iron")
            ,Init("iron-bar","raw-iron:1",10,3f)

            ,Init("raw-copper")
            ,Init("copper-bar","raw-copper:1",10,3f)

            ,Init("raw-stone")
            ,Init("brick","raw-stone:1",10,1f)


            ,Init("dirty-water")
            ,Init("clean-water","dirty-water:1",10,3f)

            ,Init("raw-rice")
            ,Init("rice", "raw-rice:1,clean-water:1", 10,3f)
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