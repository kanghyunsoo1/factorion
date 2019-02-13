using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUnloader :MonoBehaviour {

    void Start() {
        StaticDatas.wasMainLoad = true;
    }
}
