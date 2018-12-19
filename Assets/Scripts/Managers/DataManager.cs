using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    GUIManager guim;
    void Start()
    {
        guim = GetComponent<GUIManager>();
    }

    public void Save(string mapName)
    {
        StartCoroutine(_Save(mapName));
    }

    private IEnumerator _Save(string mapName)
    {
        guim.OnSaveStart();
        yield return null;
        int i = 0;
        foreach (SavableObject so in FindObjectsOfType<SavableObject>())
        {
            so.BeforeSave();
            PlayerPrefs.SetString(mapName + "_" + i, so.ToJson());
            PlayerPrefs.SetString("name_" + i, so.name.Replace("(Clone)", ""));
            i = i + 1;
        }
        foreach (SaveAndLoader sal in FindObjectsOfType<SaveAndLoader>())
        {
            sal.Save(mapName);
        }
        PlayerPrefs.Save();
        guim.OnSaveEnd();
    }

    public void Load(string mapName)
    {
        StartCoroutine(_Load(mapName));
    }

    private IEnumerator _Load(string mapName)
    {
        guim.OnLoadStart();
        yield return null;
        foreach (SavableObject so in FindObjectsOfType<SavableObject>())
        {
            Destroy(so.gameObject);
        }
        int i = 0;
        while (true)
        {
            try
            {
                if (PlayerPrefs.HasKey(mapName + "_" + i))
                {
                    var name = PlayerPrefs.GetString("name_" + i);
                    var go = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Savables/" + name + ".prefab", typeof(GameObject)));
                    var savable = go.GetComponent<SavableObject>();
                    JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(mapName + "_" + i), savable);
                    savable.AfterLoad();
                }
                else
                {
                    break;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.StackTrace);
            }
            i++;
        }
        foreach (SaveAndLoader sal in FindObjectsOfType<SaveAndLoader>())
        {
            sal.Load(mapName);
        }
        guim.OnLoadEnd();
    }

    public void Clean(string mapName)
    {

        int i = 0;
        while (true)
        {

            if (PlayerPrefs.HasKey(mapName + "_" + i))
            {
                PlayerPrefs.DeleteKey(mapName + "_" + i);
            }
            else
            {
                break;
            }
            i++;
        }

        foreach (SaveAndLoader sal in FindObjectsOfType<SaveAndLoader>())
        {
            sal.Clean(mapName);
        }
    }
}
