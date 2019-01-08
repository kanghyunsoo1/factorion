using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotUser :MonoBehaviour {

    private RobotManager _rm;

    private void Start() {
        _rm = FindObjectOfType<RobotManager>();
    }
}
