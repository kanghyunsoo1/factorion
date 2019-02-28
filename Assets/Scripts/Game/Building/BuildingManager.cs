using System;
using UnityEngine;

public class BuildingManager :MonoBehaviour {
    public BuildingInfo[] buildingInfos;

    private void Awake() {
        buildingInfos = new BuildingInfo[] {
             Init("belt","iron:10")
            ,Init("mill","iron:10, copper:10")
        };
    }

    private BuildingInfo Init(string name, string needs) {
        return new BuildingInfo(name, needs);
    }

    public BuildingInfo GetBuildingInfo(string name) {
        return Array.Find(buildingInfos, x => x.name.Equals(name));
    }
}