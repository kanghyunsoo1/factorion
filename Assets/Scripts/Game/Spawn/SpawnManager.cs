using System;
using System.Collections;
using UnityEngine;

public class SpawnManager :Manager {
    public SpawnableInfo[] spawnableInfos;

    private SpinnerManager _spinnerManager;
    private RiceCakeManager _riceCakeManager;

    void Awake() {
        _riceCakeManager = ManagerManager.GetManager<RiceCakeManager>();
        _spinnerManager = ManagerManager.GetManager<SpinnerManager>();

        spawnableInfos = new SpawnableInfo[]{
             new SpawnableInfo(){ name="rice",chance=0.001f, minRange = 4f }
            ,new SpawnableInfo(){ name="water",chance=0.001f, minRange = 4f }
            ,new SpawnableInfo(){ name="coal",chance=0.002f, minRange = 2f }
            ,new SpawnableInfo(){ name="iron",chance=0.001f, minRange = 2f }
            ,new SpawnableInfo(){ name="copper",chance=0.001f, minRange = 2f }
            ,new SpawnableInfo(){ name="stone",chance=0.002f, minRange = 3f }
        };
    }

    public SpawnableInfo GetSpawnableInfo(string name) {
        return Array.Find(spawnableInfos, x => x.name.Equals(name));
    }

    public void Spawn() {
        StartCoroutine(_Spawn());
    }

    private IEnumerator _Spawn() {
        _spinnerManager.SpinnerOn("spawn");
        yield return null;
        for (int i = -StaticDatas.SIZE; i < StaticDatas.SIZE; i++) {
            for (int j = -StaticDatas.SIZE; j < StaticDatas.SIZE; j++) {
                foreach (SpawnableInfo ri in spawnableInfos) {
                    float range = Vector2.Distance(new Vector2(i, j), Vector2.zero);
                    if (range < ri.minRange)
                        continue;
                    if (UnityEngine.Random.Range(0f, 1f) <= ri.chance) {
                        GameObject go = _riceCakeManager.Instantiate(ri.name);
                        go.transform.position = new Vector3(i, 0, j);
                        break;
                    }
                }
            }
        }
        _spinnerManager.SpinnerOff();
    }
}