using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOResource :SavableObject {
    public int resourceIndex;
    public int amount;

    private ResourceManager _rm;

    private void Start() {
        _rm = FindObjectOfType<ResourceManager>();
        var ri = _rm.resourceInfos[resourceIndex];
        var color = new Color(ri.r, ri.g, ri.b); ;
        GetComponent<MinimapEntity>().color = color;
        GetComponent<SpriteRenderer>().color = color;
    }

}
