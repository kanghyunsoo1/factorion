using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    void Start()
    {

    }

    public void SaveAll(string mapName)
    {
        StartCoroutine(_SaveAll(mapName));
    }

    private IEnumerator _SaveAll(string mapName)
    {
        GetComponent<GUIManager>().OnSaveStart();
        yield return null;
        int i = 0;
        foreach (SavableObject so in FindObjectsOfType<SavableObject>())
        {
            PlayerPrefs.SetString(mapName + "_" + i, so.ToJson());
            PlayerPrefs.SetString("name_" + i, so.name.Replace("(Clone)",""));
            Debug.Log(PlayerPrefs.GetString(mapName + "_" + i));
            i = i + 1;
        }
        GetComponent<GUIManager>().OnSaveEnd();
    }

    public void Load(string mapName)
    {
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
                    Debug.Log(name);
                    var go = (GameObject)Instantiate(AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Savables/" + name+".prefab", typeof(GameObject)));
                    var savable = go.GetComponent<SavableObject>();
                    JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(mapName + "_" + i),savable);
                    savable.RestoreBaseVar();
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
    }
}
