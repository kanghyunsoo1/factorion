﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner :KhsComponent {
    private ValueManager _vm;
    private Resource _resource;
    private InventoryHolder _holder;
    void Awake() {
        _resource = GetComponent<Resource>();
        _holder = GetComponent<InventoryHolder>();
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
            if (_holder.GetItemCount(_resource.name) < c) {
                _holder.AddItem(_resource.name, (int)a);
                _resource.amount -= (int)a;
            }
            if (_resource.amount <= 0) {
                StopAllCoroutines();
            }
        }
    }

}
