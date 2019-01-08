using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoader :MonoBehaviour {
    private string _mapName;
    private string _key;
    private void Start() {
        _mapName = StaticDatas.mapName;
        _key = ToString();
    }

    public void Save() {
        string a = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(_mapName + _key, a);
    }

    public void Load() {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(_mapName + _key), this);
    }

    public void Clean() {
        PlayerPrefs.DeleteKey(_mapName + "_SaveAndLoader_" + _key);
    }
}
