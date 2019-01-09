using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager :MonoBehaviour {

    [Serializable]
    public class Wraper {
        public ItemInfo[] v;
    }

    public ItemInfo[] itemInfos;

    private void Awake() {
        var tempInfos = JsonUtility.FromJson<Wraper>(Resources.Load<TextAsset>("item").text).v;
        itemInfos = new ItemInfo[tempInfos.Length];
        for (int i = 0; i < tempInfos.Length; i++) {
            itemInfos[i] = tempInfos[i];
            itemInfos[i].sprite = Resources.Load<Sprite>("Sprites/" + tempInfos[i].nameKey);
        }
    }

    void Start() {

    }

    void Update() {

    }
}