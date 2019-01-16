using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner :KhsComponent {
    public int storedAmount;
    private ValueManager _vm;
    private Resource _resource;
    void Awake() {
        _resource = GetComponent<Resource>();
        _vm = FindObjectOfType<ValueManager>();
    }

    void Start() {
        StartCoroutine(Mine());
    }

    IEnumerator Mine() {
        while (true) {
            yield return new WaitForSeconds(_vm.GetValue("minerDelay").Value);
            var a = _vm.GetValue("minerAmount").Value;
            var c = _vm.GetValue("minerCapacity").Value;
            if (storedAmount < c) {
                storedAmount += (int)a;
                _resource.amount -= (int)a;
            }
            if (_resource.amount <= 0) {
                StopAllCoroutines();
            }
        }
    }

}
