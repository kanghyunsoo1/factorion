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

    void Start() {
        itemInfos = JsonUtility.FromJson<Wraper>(Resources.Load<TextAsset>("item").text).v;
        
    }

    void Update() {

    }
}