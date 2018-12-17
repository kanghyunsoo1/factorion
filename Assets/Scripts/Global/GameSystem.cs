using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    string mapName;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        mapName = "map0";
        
    }
    public IEnumerator SaveAll()
    {
        GetComponent<GUIManager>().OnSaveStart();
        yield return null;
        int i = 0;
        foreach (SavableObject so in FindObjectsOfType<SavableObject>())
        {
            PlayerPrefs.SetString(mapName+"_" + i, so.ToJson());
            Debug.Log(PlayerPrefs.GetString(mapName + "_" + i));
            i = i + 1;
        }
        GetComponent<GUIManager>().OnSaveEnd();
    }
}