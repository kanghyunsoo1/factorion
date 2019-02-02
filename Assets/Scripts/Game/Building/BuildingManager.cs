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
            , requiredItems = new string[]{"iron","copper"}
            , requiredCounts = new int[]{ 10,5} }
        };
    }


    public BuildingInfo GetBuildingInfo(string name) {
        return Array.Find(buildingInfos, x => x.name.Equals(name));
    }
}