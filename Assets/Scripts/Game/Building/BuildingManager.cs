using System;

public class BuildingManager :Manager {
    public BuildingInfo[] buildingInfos;

    private void Awake() {
        buildingInfos = new BuildingInfo[] {
            Init("inserter","iron-bar:10")
            ,Init("fan","iron-bar:10")
            ,Init("miner","iron-bar:30")
            ,Init("millstone","iron-bar:30, copper-bar:20")
            ,Init("furance","iron-bar:10")
            ,Init("purifier","iron-bar:10,copper-bar:10")
            ,Init("generator","iron-bar:10,copper-bar:10")
        };
    }

    private BuildingInfo Init(string name, string needs) {
        return new BuildingInfo(name, needs);
    }

    public BuildingInfo GetBuildingInfo(string name) {
        return Array.Find(buildingInfos, x => x.name.Equals(name));
    }
}