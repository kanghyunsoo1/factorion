using System;

public class ValueManager :Manager {
    [Serializable]
    public class Wraper {
        public UpgradableValue[] v;
    }
    public UpgradableValue[] values;

    private RiceCakeManager _riceCakeManager;
    void Awake() {
        _riceCakeManager = ManagerManager.GetManager<RiceCakeManager>();
        values = new UpgradableValue[] {
             Init("gustn",             100f)
            ,Init("maxRobotCount",     10f,    5f,     100,    503,    50 )
            ,Init("robotSpeed",        1f,     0.25f,  40,     50,     50 )
            ,Init("robotCapacity",     5f,     1f,     100,    50,     50 )
            ,Init("minerCapacity",     200f,   50f,    100,    40,     40 )
            ,Init("minerDelay",        1.1f,   -0.01f, 100,    30,     30 )
            ,Init("minerAmount",       5f,     1f,     100,    50,     50 )
        };
    }

    private UpgradableValue Init(string name, float defaultValue, float deltaValue = 0f, int maxUpgradeCount = 0, int defaultPirce = 0, int deltaPrice = 0) {
        var go = _riceCakeManager.Instantiate("value");
        var uv = go.GetComponent<UpgradableValue>();
        uv.name = name;
        uv.defaultValue = defaultValue;
        uv.deltaValue = deltaValue;
        uv.maxUpgradeCount = maxUpgradeCount;
        uv.defaultPrice = defaultPirce;
        uv.deltaPrice = deltaPrice;
        return uv;
    }

    public UpgradableValue GetValue(string name) {
        return Array.Find(values, x => x.name.Equals(name));
    }

    public UpgradableValue[] GetUpgradableValues() {
        return Array.FindAll(values, x => x.maxUpgradeCount > 0);
    }

    public UpgradableValue[] GetUnupgradableValues() {
        return Array.FindAll(values, x => x.maxUpgradeCount == 0);
    }
}