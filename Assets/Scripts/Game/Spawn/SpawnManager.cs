using System;
using System.Collections;
using UnityEngine;

public class SpawnManager :MonoBehaviour {
    public SpawnableInfo[] spawnableInfos;

    private SpinnerManager _spinnerManager;
    private RiceCakeManager _riceCakeManager;

    void Awake() {
        _riceCakeManager = GetComponent<RiceCakeManager>();
        _spinnerManager = GetComponent<SpinnerManager>();

        spawnableInfos = new SpawnableInfo[]{
             new SpawnableInfo(){ name="rice",chance=0.001f, minRange = 5f }
            ,new SpawnableInfo(){ name="water",chance=0.001f, minRange = 5f }
            ,new SpawnableInfo(){ name="coal",chance=0.001f, minRange = 5f }
            ,new SpawnableInfo(){ name="iron",chance=0.001f, minRange = 5f }
            ,new SpawnableInfo(){ name="copper",chance=0.001f, minRange = 5f }
            ,new SpawnableInfo(){ name="dudxo",chance=0.001f, minRange = 20f }
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
                        go.transform.position = new Vector3(i, j, 0);
                        break;
                    }
                }
            }
        }
        _spinnerManager.SpinnerOff();
    }
}