using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoader :MonoBehaviour {
    private string mapName;
    private string key;
    private void Start() {
        mapName = StaticDatas.mapName;
        key = ToString();
    }

    public void Save() {
        string a = JsonUtility.ToJson(this);
        Debug.Log(a);
        PlayerPrefs.SetString(mapName + key,a);
    }

    public void Load() {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(mapName + key), this);
    }

    public void Clean() {
        PlayerPrefs.DeleteKey(mapName + "_SaveAndLoader_" + key);
    }
}
