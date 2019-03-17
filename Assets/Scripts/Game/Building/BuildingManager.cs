using System;

public class BuildingManager :Manager {
    public BuildingInfo[] buildingInfos;

    private void Awake() {
        buildingInfos = new BuildingInfo[] {
             Init("fan",10)
            ,Init("wall",10)
            ,Init("inserter",10)
            ,Init("miner",40,2)
            ,Init("millstone",30,3)
            ,Init("furance",30,2)
            ,Init("purifier",30,3)
            ,Init("generator",50,0)
        };
    }

    private BuildingInfo Init(string name, int price, int power = 1) {
        return new BuildingInfo() { name = name, price = price, power = power };
    }

    public BuildingInfo GetBuildingInfo(string name) {
        return Array.Find(buildingInfos, x => x.name.Equals(name));
    }
}