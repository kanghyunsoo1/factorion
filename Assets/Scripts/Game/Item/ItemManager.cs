using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager :MonoBehaviour {
    public ItemInfo[] itemInfos;

    private void Awake() {
        itemInfos = new ItemInfo[]{
            new ItemInfo() { name = "coal" }
            ,new ItemInfo() { name = "iron" }
            ,new ItemInfo() { name = "copper" }
            ,new ItemInfo() { name = "tin" }
            ,new ItemInfo() { name = "dudxo" }
            ,new ItemInfo() { name = "iron-bar", recipe = new ItemRecipe(){ stack = new ItemStack(){name="iron",count =5} , type=ItemRecipe.Type.Burn } }
            ,new ItemInfo() { name = "copper-bar",recipe = new ItemRecipe(){ stack = new ItemStack(){name="copper",count =5} , type=ItemRecipe.Type.Burn } }
            ,new ItemInfo() { name = "tin-bar" ,recipe = new ItemRecipe(){ stack = new ItemStack(){name="tin",count =5} , type=ItemRecipe.Type.Burn } }
            ,new ItemInfo() { name = "dudxo-bar", recipe = new ItemRecipe(){ stack = new ItemStack(){name="dudxo",count =5} , type=ItemRecipe.Type.Burn } }
        };
    }

    void Start() {

    }


    public ItemInfo GetItemInfo(string name) {
        return Array.Find(itemInfos, x => x.name.Equals(name));
    }
}