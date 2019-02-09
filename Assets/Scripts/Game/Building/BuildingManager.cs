using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager :MonoBehaviour {
    public BuildingInfo[] buildingInfos;

    private void Awake() {
        buildingInfos = new BuildingInfo[] {
            new BuildingInfo(){
                name ="miner"
            , requiredSlots = new InventorySlot[]{
                new InventorySlot(){ name = "iron",count = 10 }
                ,new InventorySlot(){ name = "copper",count = 10 }
                }
            }
            ,new BuildingInfo(){
                name ="assembler"
            , requiredSlots = new InventorySlot[]{
                new InventorySlot(){ name = "iron",count = 10 }
                ,new InventorySlot(){ name = "copper",count = 10 }
                ,new InventorySlot(){ name = "tin",count = 10 }
                }
            }
        };
    }


    public BuildingInfo GetBuildingInfo(string name) {
        return Array.Find(buildingInfos, x => x.name.Equals(name));
    }
}