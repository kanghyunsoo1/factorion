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
            , requiredSlots = new ItemStack[]{
                new ItemStack(){ name = "iron",count = 10 }
                ,new ItemStack(){ name = "copper",count = 10 }
                }
            }
            ,new BuildingInfo(){
                name ="assembler"
            , requiredSlots = new ItemStack[]{
                new ItemStack(){ name = "iron",count = 10 }
                ,new ItemStack(){ name = "copper",count = 10 }
                ,new ItemStack(){ name = "tin",count = 10 }
                }
            }
        };
    }


    public BuildingInfo GetBuildingInfo(string name) {
        return Array.Find(buildingInfos, x => x.name.Equals(name));
    }
}