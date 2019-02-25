using System.Collections;
using UnityEngine;

public class Miner :RiceCakeComponent {
    private ValueManager _valueManager;
    private Resource _resource;
    private Inventory _inventory;
    void Awake() {
        _resource = GetComponent<Resource>();
        _inventory = GetComponent<Inventory>();
        _valueManager = FindObjectOfType<ValueManager>();
    }

    void Start() {
        StartCoroutine(Mine());
        transform.Find("resource").GetComponent<SpriteRenderer>().sprite = FindObjectOfType<SpriteManager>().GetSprite("block", _resource.name);
    }

    IEnumerator Mine() {
        while (true) {
            yield return new WaitForSeconds(_valueManager.GetValue("minerDelay").Value);
            var a = _valueManager.GetValue("minerAmount").Value;
            var c = _valueManager.GetValue("minerCapacity").Value;
            if (_inventory.GetItemCount(_resource.name) < c) {
                _inventory.AddItem(_resource.name, (int)a);
                _resource.amount -= (int)a;
            }
            if (_resource.amount <= 0) {
                StopAllCoroutines();
            }
        }
    }
}