using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResourceManager :MonoBehaviour {
    public ResourceInfo[] resourceInfos;
    private GuiManager _guim;
    void Awake() {
        resourceInfos = new ResourceInfo[] {
            new ResourceInfo{ name="coal", minAmount=1000, maxAmount=5000, rangeFactor=200,chance=0.001f,minRange=5f }
            ,new ResourceInfo{ name="iron", minAmount=1000, maxAmount=5000, rangeFactor=200,chance=0.001f,minRange=5f }
            ,new ResourceInfo{ name="copper", minAmount=1000, maxAmount=5000, rangeFactor=200,chance=0.001f,minRange=5f }
            ,new ResourceInfo{ name="tin", minAmount=1000, maxAmount=5000, rangeFactor=150,chance=0.001f,minRange=10f }
            ,new ResourceInfo{ name="lead", minAmount=1000, maxAmount=5000, rangeFactor=130,chance=0.001f,minRange=20f }
            ,new ResourceInfo{ name="dudxo", minAmount=500, maxAmount=2000, rangeFactor=50,chance=0.001f,minRange=30f }

        };
        _guim = GetComponent<GuiManager>();
    }

    public ResourceInfo GetResourceInfo(string name) {
        return Array.Find(resourceInfos, x => x.name.Equals(name));
    }

    public void Spawn() {
        StartCoroutine(_Spawn());
    }

    private IEnumerator _Spawn() {
        _guim.OnSpawnStart();
        yield return null;
        for (int i = -StaticDatas.SIZE; i < StaticDatas.SIZE; i++) {
            for (int j = -StaticDatas.SIZE; j < StaticDatas.SIZE; j++) {
                foreach (ResourceInfo ri in resourceInfos) {
                    float range = Vector2.Distance(new Vector2(i, j), Vector2.zero);
                    if (range < ri.minRange)
                        continue;
                    int chance = (int)(ri.chance * 1000);
                    if (UnityEngine.Random.Range(0, 1000) <= chance) {

                        GameObject go = Instantiate(Resources.Load<GameObject>("KhsObjects/BlockResource"));
                        go.transform.position = new Vector3(i, j, 0);
                        go.transform.Rotate(0f, 0f, UnityEngine.Random.Range(0f, 360f));
                        var resource = go.GetComponent<Resource>();
                        resource.name = ri.name;
                        resource.amount = (int)(ri.rangeFactor * range) + UnityEngine.Random.Range(ri.minAmount, ri.maxAmount + 1);
                        break;
                    }
                }
            }
        }

        _guim.OnSpawnEnd();
    }


}