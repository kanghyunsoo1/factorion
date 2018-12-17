using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    DataManager dm;
    ResourceManager rm;
    void Start()
    {
        dm = GetComponent<DataManager>();
        rm = GetComponent<ResourceManager>();

        Debug.Log("First: "+StaticDatas.isFirst);
        if (StaticDatas.isFirst)
        {
            rm.Spawn();
        }
        else
        {
            dm.Load(StaticDatas.mapName);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            dm.Save(StaticDatas.mapName);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            dm.Load(StaticDatas.mapName);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            dm.Clean(StaticDatas.mapName);
        }
    }

}