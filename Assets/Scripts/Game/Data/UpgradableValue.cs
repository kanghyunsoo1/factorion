using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableValue :SavableObject {
    public string valueName;
    public float defaultValue;
    public float deltaValue;
    public int upgradeCount;
    public int maxUpgradeCount;
    public int defaultPrice;
    public int deltaPrice;
    public Sprite sprite;

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
