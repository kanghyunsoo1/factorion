using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager :MonoBehaviour {
    public ItemInfo[] itemInfos;

    private void Awake() {
        itemInfos = new ItemInfo[]{
            new ItemInfo() { name = "coal" }
            ,new ItemInfo() { name = "raw_iron" }
            ,new ItemInfo() { name = "raw_copper" }
            ,new ItemInfo() { name = "raw_tin" }
            ,new ItemInfo() { name = "raw_lead" }
            ,new ItemInfo() { name = "raw_dudxo",size = 10 }
            ,new ItemInfo() { name = "iron" }
            ,new ItemInfo() { name = "copper" }
            ,new ItemInfo() { name = "tin" }
            ,new ItemInfo() { name = "lead" }
            ,new ItemInfo() { name = "dudxo" }
        };
    }

    void Start() {

    }


    public ItemInfo GetItemInfo(string name) {
        return Array.Find(itemInfos, x => x.name.Equals(name));
    }
}