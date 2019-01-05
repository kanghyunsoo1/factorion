using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource :SavableObject {
    public int ResourceId;
    public int Amount;

    private ResourceManager rm;

    private void Start() {
        rm = FindObjectOfType<ResourceManager>();
        var ri = rm.GetResourceInfo(ResourceId);
        var color = new Color(ri.r, ri.g, ri.b); ;
        GetComponent<MinimapEntity>().Color = color;
        GetComponent<SpriteRenderer>().color = color;
    }

}
