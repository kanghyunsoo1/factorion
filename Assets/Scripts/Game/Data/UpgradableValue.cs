using System;
[Serializable]
public class UpgradableValue :KhsComponent {
    public new string name;
    public float defaultValue;
    public float deltaValue;
    public int upgradeCount;
    public int maxUpgradeCount;
    public int defaultPrice;
    public int deltaPrice;

    public float Value {
        get {
            return deltaValue * upgradeCount + defaultValue;
        }
    }
    public float Price {
        get {
            return deltaPrice * upgradeCount + defaultPrice;
        }
    }
}