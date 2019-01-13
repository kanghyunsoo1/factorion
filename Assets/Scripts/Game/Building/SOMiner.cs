using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOMiner :SOResource {
    public int storedAmount;
    private ValueManager _vm;

    new void Awake() {
        base.Awake();
        _vm = FindObjectOfType<ValueManager>();
    }

    new void Start() {
        base.Start();
        GetComponent<SpriteRenderer>().color = Color.white;
        StartCoroutine(Mine());
    }

    IEnumerator Mine() {
        while (true) {
            yield return new WaitForSeconds(_vm.GetValue("minerDelay").Value);
            var a = _vm.GetValue("minerAmount").Value;
            var c = _vm.GetValue("minerCapacity").Value;
            if (storedAmount < c) {
                storedAmount += (int)a;
                amount -= (int)a;
            }
            if (amount <= 0) {
                StopAllCoroutines();
            }
        }
    }

}
