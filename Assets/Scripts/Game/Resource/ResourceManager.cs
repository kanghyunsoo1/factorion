using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResourceManager :MonoBehaviour {
    [Serializable]
    public class Wraper {
        public ResourceInfo[] v;
    }
    public ResourceInfo[] resourceInfos;
    private GuiManager _guim;
    void Start() {
        resourceInfos = JsonUtility.FromJson<Wraper>(Resources.Load<TextAsset>("resource").text).v;
        _guim = GetComponent<GuiManager>();
    }

    public void Spawn() {
        StartCoroutine(_Spawn());
    }

    private IEnumerator _Spawn() {
        _guim.OnSpawnStart();
        yield return null;
        for (int i = -StaticDatas.SIZE; i < StaticDatas.SIZE; i++) {
            for (int j = -StaticDatas.SIZE; j < StaticDatas.SIZE; j++) {
                int index = 0;
                foreach (ResourceInfo ri in resourceInfos) {

                    float range = Vector2.Distance(new Vector2(i, j), Vector2.zero);
                    if (range < ri.minRange)
                        continue;
                    int chance = (int)(ri.chance * 1000);
                    if (UnityEngine.Random.Range(0, 1000) <= chance) {

                        GameObject go = Instantiate(Resources.Load<GameObject>("Savables/Resource"));
                        go.transform.position = new Vector3(i, j, 0);
                        go.transform.Rotate(0f, 0f, UnityEngine.Random.Range(0f, 360f));
                        var res = go.GetComponent<SOResource>();
                        res.resourceIndex = index;
                        res.amount = (int)(ri.rangeFactor * range) + UnityEngine.Random.Range(ri.minAmount, ri.maxAmount + 1);
                        break;
                    }
                    index++;
                }
            }
        }

        _guim.OnSpawnEnd();
    }


}