using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Slot :MonoBehaviour {
    public int index;

    void Start() {
        transform.Find("Text").GetComponent<Text>().text = "Slot " + index + 1;
    }

    public void OnClick() {
        StaticDatas.mapName = "slot" + index;
        SceneManager.LoadScene("Game");
    }
}
