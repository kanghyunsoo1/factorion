using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoader :MonoBehaviour {
    private string mapName;
    private void Start() {
        mapName = StaticDatas.mapName;
    }

    public virtual void Save() {

    }

    public virtual void Load() {

    }

    public virtual void Clean() {

    }
}
