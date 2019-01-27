using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ValueManager :MonoBehaviour {
    [Serializable]
    public class Wraper {
        public UpgradableValue[] v;
    }
    public UpgradableValue[] values;
    void Awake() {
        values = new UpgradableValue[] {
            new UpgradableValue() { name = "gustn", defaultValue = 100f }
            ,new UpgradableValue() { name = "maxRobotCount", defaultValue = 10f, deltaValue = 5f, maxUpgradeCount = 100, defaultPrice = 50, deltaPrice = 50 }
            ,new UpgradableValue() { name = "minerCapacity", defaultValue = 200f, deltaValue = 50f, maxUpgradeCount = 100, defaultPrice = 40, deltaPrice = 40 }
            ,new UpgradableValue() { name = "minerDelay", defaultValue = 1.1f, deltaValue = -0.01f, maxUpgradeCount = 100, defaultPrice = 30, deltaPrice = 30 }
            ,new UpgradableValue() { name = "minerAmount", defaultValue = 5f, deltaValue = 1f, maxUpgradeCount = 100, defaultPrice = 50, deltaPrice = 50 }
        };
        RefreshSprite();

    }

    public void RefreshSprite() {
        foreach (var v in values) {
            v.sprite = Resources.Load<Sprite>("Sprites/val_" + v.name);
        }
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


    public void Save(string name) {
        var path = Application.persistentDataPath + "/" + name;
        Directory.CreateDirectory(path);
        var sb = new StringBuilder();
        sb.Append("{\"v\":[");
        foreach (var v in values) {
            sb.Append(JsonUtility.ToJson(v));
            sb.Append(",");
        }
        sb.Remove(sb.Length - 1, 1);
        sb.Append("]}");
        File.WriteAllText(path + "/values.khs", sb.ToString());
    }

    public void Load(string name) {
        var path = Application.persistentDataPath + "/" + name;
        if (!File.Exists(Application.persistentDataPath + "/" + name + "/values.khs"))
            return;
        var a = JsonUtility.FromJson<Wraper>(File.ReadAllText(path + "/values.khs"));
        values = a.v;
        RefreshSprite();
    }

    public void Delete(string name) {
        var path = Application.persistentDataPath + "/" + name;
        File.Delete(path + "/values.khs");
    }

}
