using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceIniter :MonoBehaviour {

    private ResourceManager _rm;
    private Resource _resource;
    public void Awake() {
        _rm = FindObjectOfType<ResourceManager>();
        _resource = GetComponent<Resource>();
    }

    public void Start() {
        var ri = _rm.resourceInfos[_resource.resourceIndex];
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/block_" + ri.name);
        Destroy(this);
    }
}
