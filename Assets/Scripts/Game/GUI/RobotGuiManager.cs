using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotGuiManager :MonoBehaviour {
    public GameObject robotGuiObject;
    private RobotManager _rm;
    private ValueManager _vm;
    private Text _panelText;

    void Awake() {
        _panelText = robotGuiObject.transform.Find("Panel").Find("Text").GetComponent<Text>();
        _vm = GetComponent<ValueManager>();
        _rm = GetComponent<RobotManager>();
    }

    void FixedUpdate() {
        int ready = _rm.GetReadyCount();
        int active = _rm.GetActiveCount();
        int max = (int)_vm.GetValue("maxRobotCount").Value;
        int all = ready + active;
        string cAll = "black";
        if (all >= max) {
            cAll = "red";
        } else if (all >= ((float)max * 0.7f)) {
            cAll = "orange";
        }
        _panelText.text = string.Format("<color='{0}'>{1}</color> / {2}\n{3}", cAll, all, max, active);
    }

}
