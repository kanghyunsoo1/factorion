using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotUser :MonoBehaviour {

    private RobotManager _rm;

    private void Awake() {
        _rm = FindObjectOfType<RobotManager>();
    }
}
