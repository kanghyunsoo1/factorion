using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [Serializable]
    public class Wraper
    {
        public ResourceInfo[] v;
    }
    public TextAsset resourceInfoText;
    public ResourceInfo[] infos;
    GUIManager guim;

    void Start()
    {
        infos = JsonUtility.FromJson<Wraper>(resourceInfoText.text).v;
        guim = GetComponent<GUIManager>();
    }

    public void Spawn()
    {
        StartCoroutine(_Spawn());
    }

    private IEnumerator _Spawn()
    {
        guim.OnSpawnStart();
        yield return null;
        for (int i = -StaticDatas.WIDTH; i < StaticDatas.WIDTH; i++)
        {
            for (int j = -StaticDatas.HEIGHT; j < StaticDatas.HEIGHT; j++)
            {
                foreach (ResourceInfo ri in infos)
                {
                    float range = Vector2.Distance(new Vector2(i, j), Vector2.zero);
                    if (range < ri.minRange)
                        continue;
                    int chance = (int)(ri.chance * 100);
                    if (UnityEngine.Random.Range(0, 100)  <= chance)
                    {
                        GameObject go = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Savables/Resource" + ri.resName + ".prefab"));
                        go.transform.position = new Vector3(i,j,0);
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