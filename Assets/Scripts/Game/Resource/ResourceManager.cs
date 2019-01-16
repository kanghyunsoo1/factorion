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
    void Awake() {
        resourceInfos = JsonUtility.FromJson<Wraper>(Resources.Load<TextAsset>("resource").text).v;
        foreach (var ri in resourceInfos) {
            ri.sprite = Resources.Load<Sprite>("Sprites/block_" + ri.name);
        }
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

                        GameObject go = Instantiate(Resources.Load<GameObject>("KhsObjects/BlockResource"));
                        go.transform.position = new Vector3(i, j, 0);
                        go.transform.Rotate(0f, 0f, UnityEngine.Random.Range(0f, 360f));
                        var resource = go.GetComponent<Resource>();
                        resource.resourceIndex = index;
                        resource.amount = (int)(ri.rangeFactor * range) + UnityEngine.Random.Range(ri.minAmount, ri.maxAmount + 1);
                        break;
                    }
                    index++;
                }
            }
        }

        _guim.OnSpawnEnd();
    }


}