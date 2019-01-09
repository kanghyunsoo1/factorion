using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueManager :MonoBehaviour {

    private UpgradableValue[] _values;
    void Awake() {
        _values = FindObjectsOfType<UpgradableValue>();
    }

    public void RefreshValues() {
        _values = FindObjectsOfType<UpgradableValue>();
    }

    public UpgradableValue GetValue(string name) {
        return Array.Find(_values, x => x.valueName.Equals(name));
    }

    public UpgradableValue[] GetUpgradableValues() {
        return Array.FindAll(_values, x => x.maxUpgradeCount > 0);
    }

    public UpgradableValue[] GetUnupgradableValues() {
        return Array.FindAll(_values, x => x.maxUpgradeCount == 0);
    }
}
