using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager :MonoBehaviour {
    GUIManager guim;
    string mapName;
    void Start() {
        guim = GetComponent<GUIManager>();
        mapName = StaticDatas.mapName;
    }

    public void Save() {
        StartCoroutine(_Save());
    }

    private IEnumerator _Save() {
        guim.OnSaveStart();
        yield return null;
        int i = 0;

        PlayerPrefs.SetInt(mapName, 12341234);
        foreach (SavableObject so in FindObjectsOfType<SavableObject>()) {
            so.BeforeSave();
            PlayerPrefs.SetString(mapName + "_" + i, so.ToJson());
            PlayerPrefs.SetString(mapName + "_name_" + i, so.name.Replace("(Clone)", ""));
            i = i + 1;
        }
        foreach (SaveAndLoader sal in FindObjectsOfType<SaveAndLoader>()) {
            sal.Save();
        }
        PlayerPrefs.Save();
        guim.OnSaveEnd();
    }

    public void Load() {
        StartCoroutine(_Load());
    }
    public bool IsFirst() {
        return PlayerPrefs.GetInt(mapName) != 12341234;
    }
    private IEnumerator _Load() {
        guim.OnLoadStart();
        yield return null;
        if (IsFirst()) {
            Application.Quit();
            Debug.Log("Is First!!!!!");
        }
        foreach (SavableObject so in FindObjectsOfType<SavableObject>()) {
            Destroy(so.gameObject);
        }
        int i = 0;
        while (true) {
            try {
                if (PlayerPrefs.HasKey(mapName + "_" + i)) {
                    var name = PlayerPrefs.GetString(mapName + "_name_" + i);
                    var go = (GameObject)Instantiate(Resources.Load<GameObject>("Savables/" + name));
                    var savable = go.GetComponent<SavableObject>();
                    JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(mapName + "_" + i), savable);
                    savable.AfterLoad();
                } else {
                    break;
                }
            } catch (Exception e) {
                Debug.Log(e.StackTrace);
            }
            i++;
        }
        foreach (SaveAndLoader sal in FindObjectsOfType<SaveAndLoader>()) {
            sal.Load();
        }
        guim.OnLoadEnd();
    }

    public void Clean() {

        PlayerPrefs.DeleteKey(mapName);
        int i = 0;
        while (true) {
            if (PlayerPrefs.HasKey(mapName + "_" + i)) {
                PlayerPrefs.DeleteKey(mapName + "_" + i);
                PlayerPrefs.DeleteKey(mapName + "_name_" + i);
            } else {
                break;
            }
            i++;
        }

        foreach (SaveAndLoader sal in FindObjectsOfType<SaveAndLoader>()) {
            sal.Clean();
        }
        PlayerPrefs.Save();
    }
}
