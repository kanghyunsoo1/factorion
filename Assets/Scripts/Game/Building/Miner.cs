using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner :KhsComponent {
    private ValueManager _vm;
    private Resource _resource;
    private Inventory _inventory;
    void Awake() {
        _resource = GetComponent<Resource>();
        _inventory = GetComponent<Inventory>();
        _vm = FindObjectOfType<ValueManager>();
    }

    void Start() {
        StartCoroutine(Mine());
        transform.Find("resource").GetComponent<SpriteRenderer>().sprite = FindObjectOfType<SpriteManager>().GetSprite("block", _resource.name);
    }

    IEnumerator Mine() {
        while (true) {
            yield return new WaitForSeconds(_vm.GetValue("minerDelay").Value);
            var a = _vm.GetValue("minerAmount").Value;
            var c = _vm.GetValue("minerCapacity").Value;
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
