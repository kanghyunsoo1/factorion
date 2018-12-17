using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    string mapName;
    DataManager dm;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        mapName = "map0";
        dm = GetComponent<DataManager>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dm.SaveAll(mapName);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            dm.Load(mapName);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.DeleteAll();
        }
    }

}