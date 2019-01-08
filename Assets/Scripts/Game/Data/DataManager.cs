using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager :MonoBehaviour {
    private GuiManager _guim;
    private string _mapName;
    void Start() {
        _guim = GetComponent<GuiManager>();
        _mapName = StaticDatas.mapName;
    }

    public void Save() {
        StartCoroutine(_Save());
    }

    private IEnumerator _Save() {
        _guim.OnSaveStart();
        yield return null;
        int i = 0;

        PlayerPrefs.SetInt(_mapName, 12341234);
        foreach (SavableObject so in FindObjectsOfType<SavableObject>()) {
            so.BeforeSave();
            PlayerPrefs.SetString(_mapName + "_" + i, so.ToJson());
            PlayerPrefs.SetString(_mapName + "_name_" + i, so.name.Replace("(Clone)", ""));
            i = i + 1;
        }
        foreach (SaveAndLoader sal in FindObjectsOfType<SaveAndLoader>()) {
            sal.Save();
        }
        PlayerPrefs.Save();
        _guim.OnSaveEnd();
    }

    public void Load() {
        StartCoroutine(_Load());
    }
    public bool IsFirst() {
        return PlayerPrefs.GetInt(_mapName) != 12341234;
    }
    private IEnumerator _Load() {
        _guim.OnLoadStart();
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
                if (PlayerPrefs.HasKey(_mapName + "_" + i)) {
                    var name = PlayerPrefs.GetString(_mapName + "_name_" + i);
                    var go = (GameObject)Instantiate(Resources.Load<GameObject>("Savables/" + name));
                    var savable = go.GetComponent<SavableObject>();
                    JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(_mapName + "_" + i), savable);
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
        _guim.OnLoadEnd();
    }

    public void Clean() {

        PlayerPrefs.DeleteKey(_mapName);
        int i = 0;
        while (true) {
            if (PlayerPrefs.HasKey(_mapName + "_" + i)) {
                PlayerPrefs.DeleteKey(_mapName + "_" + i);
                PlayerPrefs.DeleteKey(_mapName + "_name_" + i);
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
