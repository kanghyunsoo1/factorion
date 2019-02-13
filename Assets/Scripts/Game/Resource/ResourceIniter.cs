using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceIniter :MonoBehaviour {

    private Resource _resource;
    public void Awake() {
        _resource = GetComponent<Resource>();
    }

    public void Start() {
        GetComponent<SpriteRenderer>().sprite = FindObjectOfType<SpriteManager>().GetSprite("block", _resource.name);
        Destroy(this);
    }
}
