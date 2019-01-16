using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradableValue {
    public string name;
    public float defaultValue;
    public float deltaValue;
    public int upgradeCount;
    public int maxUpgradeCount;
    public int defaultPrice;
    public int deltaPrice;
    [NonSerialized]
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
