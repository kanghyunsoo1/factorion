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
    public TextAsset ResourceInfoText;
    public ResourceInfo[] ResourceInfos;
    GUIManager guim;

    void Start() {
        ResourceInfos = JsonUtility.FromJson<Wraper>(ResourceInfoText.text).v;
        guim = GetComponent<GUIManager>();
    }

    public void Spawn() {
        StartCoroutine(_Spawn());
    }

    private IEnumerator _Spawn() {
        guim.OnSpawnStart();
        yield return null;
        for (int i = -StaticDatas.SIZE; i < StaticDatas.SIZE; i++) {
            for (int j = -StaticDatas.SIZE; j < StaticDatas.SIZE; j++) {
                foreach (ResourceInfo ri in ResourceInfos) {
                    float range = Vector2.Distance(new Vector2(i, j), Vector2.zero);
                    if (range < ri.minRange)
                        continue;
                    int chance = (int)(ri.chance * 1000);
                    if (UnityEngine.Random.Range(0, 1000) <= chance) {
                        GameObject go = Instantiate(Resources.Load<GameObject>("Savables/Resource" + ri.resName));
                        go.transform.position = new Vector3(i, j, 0);
                        go.transform.Rotate(0f, 0f, UnityEngine.Random.Range(0f, 360f));
                        var res = go.GetComponent<Resource>();
                        res.resId = ri.resId;
                        res.amount = (int)(ri.rangeFactor * range) + UnityEngine.Random.Range(ri.minAmount, ri.maxAmount + 1);
                        break;
                    }
                }
            }
        }

        guim.OnSpawnEnd();
    }

}